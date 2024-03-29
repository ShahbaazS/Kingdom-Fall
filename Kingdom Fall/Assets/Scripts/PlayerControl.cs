using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] GameObject entity; // player
    [SerializeField] GameObject struggleUI;
    [SerializeField] GameObject abilityUI;
    [SerializeField] Sprite[] sprites;

    EnemyAI enemyAI;
    EnemyPatrol enemyPatrol;
    StruggleBar struggleBar;
    //Weapon weapon;
    KnightWeapon knightWeapon;
    MageWeapon mageWeapon;
    ArcherWeapon archerWeapon;

    /*// cooldown ui stuff
    public Image icon;
    bool isCooldown = false;

    // cooldown parameters
    public float possessCooldown = 10f;
    float nextPossessTime = 0f;
    */
    public bool isPossessed = false;

    //resist parameters
    public bool resistStarted = false;

    float resistIncrement = 1f;
    float resistAmount = 2f;
    float resist = 5f;
    float possessAmount = 10f;
    float decreaseAmount = 0f;

    string activeEnemy;

    // for reseting rotation back to when not possessed
    Quaternion initialRotation;

    public void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        //weapon = GetComponent<Weapon>();

        if (GetComponent<KnightWeapon>() != null)
        {
            knightWeapon = GetComponent<KnightWeapon>();
            activeEnemy = "Knight";
        }
        else if (GetComponent<MageWeapon>() != null)
        {
            mageWeapon = GetComponent<MageWeapon>();
            activeEnemy = "Mage";
        }
        else if (GetComponent<ArcherWeapon>() != null)
        {
            archerWeapon = GetComponent<ArcherWeapon>();
            activeEnemy = "Archer";
        }

        struggleBar = struggleUI.GetComponentInChildren<StruggleBar>();
        entity.GetComponent<PlayerPossession>().icon.fillAmount = 0;
        struggleUI.SetActive(false);
    }

    public void StartPossession()
    {
        // makes sure that the cooldown is over before the player can possess
        //if (Time.time >= nextPossessTime)
        //{
            //gameObject.tag = "Player";
            entity.tag = "Untagged";

            if(enemyAI)
                enemyAI.enabled = false;
            if (enemyPatrol)
                enemyPatrol.enabled = false;

            struggleUI.SetActive(true);
            struggleBar.SetMaxStruggle(possessAmount);

            // disables player
            entity.GetComponent<PlayerMovement>().enabled = false;
            entity.SetActive(false);

            resistStarted = true;
            StartCoroutine(Resist());

        entity.GetComponent<PlayerPossession>().icon.fillAmount = 1;
        //}
    }

    void TakeOver()
    {
        gameObject.tag = "Player";
        entity.tag = "Untagged";

        abilityUI.SetActive(true);

        switch (activeEnemy) 
        {
            case "Knight":
                knightWeapon.enabled = true;
                abilityUI.GetComponent<Image>().sprite = sprites[0];
                knightWeapon.icon.sprite = sprites[0];
                break;
            case "Archer":
                archerWeapon.enabled = true;
                abilityUI.GetComponent<Image>().sprite = sprites[1];
                archerWeapon.icon.sprite = sprites[1];
                break;
            case "Mage":
                mageWeapon.enabled = true;
                abilityUI.GetComponent<Image>().sprite = sprites[2];
                mageWeapon.icon.sprite = sprites[2];
                break;
            default:
                break;
        }

        initialRotation = transform.rotation;
        struggleUI.SetActive(false);

        gameObject.layer = 8;

        if(enemyPatrol)
            GetComponent<PlayerMovement>().facingRight = enemyPatrol.isFacingRight();

        // activates the player movement script on the enemy
        GetComponent<PlayerMovement>().enabled = true;
        virtualCamera.Follow = transform; // changes the camera target

        entity.GetComponent<PlayerPossession>().AddNextPossessTime();

        entity.GetComponent<PlayerPossession>().isCooldown = true;
        StartCoroutine(entity.GetComponent<PlayerPossession>().Cooldown());
    }

    public void Eject()
    {
        gameObject.tag = "Untagged";
        entity.tag = "Player";

        abilityUI.SetActive(false);

        switch (activeEnemy)
        {
            case "Knight":
                knightWeapon.enabled = false;
                break;
            case "Archer":
                archerWeapon.enabled = false;
                break;
            case "Mage":
                mageWeapon.enabled = false;
                break;
            default:
                break;
        }

        //StartCoroutine(ActivateEnemyAI());

        transform.rotation = initialRotation;
        struggleUI.SetActive(false);

        // sets player position to the enemy position
        entity.transform.position = transform.position;

        // enables player
        entity.SetActive(true);
        entity.GetComponent<PlayerMovement>().enabled = true;

        // deactivates the player movement script on the enemy
        GetComponent<PlayerMovement>().enabled = false;
        virtualCamera.Follow = entity.transform; // changes the camera target

        isPossessed = false;

        if (resistStarted)
        {
            entity.GetComponent<PlayerPossession>().AddNextPossessTime();
            resistStarted = false;
            entity.GetComponent<PlayerPossession>().isCooldown = true;
            StartCoroutine(entity.GetComponent<PlayerPossession>().Cooldown());
        }
    }

    /*IEnumerator Cooldown()
    {
        // while the cooldown is not over
        while (isCooldown)
        {
            icon.fillAmount -= 1 / possessCooldown * Time.deltaTime;

            if (icon.fillAmount <= 0)
            {
                icon.fillAmount = 0;
                isCooldown = false;
            }

            yield return null;    // waits one frame
        }
    }*/

    IEnumerator Resist()
    {
        // while the enemy is resisting
        while (resistStarted)
        {
            // decrease by the amount enemy resists per call
            decreaseAmount = resistAmount * Time.deltaTime;
            resist -= decreaseAmount;
            struggleBar.Decrease(decreaseAmount);

            // press left mouse button to take over
            if (Input.GetMouseButtonDown(0))
                {
                resist += resistIncrement;
                struggleBar.Increase(resistIncrement);
            }

            // if you win
            if (resist >= possessAmount)
            {
                isPossessed = true;
                resistStarted = false;

                resist = 5;
                TakeOver();
            }

            // if enemy wins
            if (resist <= 0 && resistStarted)
            {
                isPossessed = false;
                //resistStarted = false;

                resist = 5;
                Eject();
            }

            yield return null;    // waits one frame
        }
    }

    IEnumerator ActivateEnemyAI()
    {
        yield return new WaitForSeconds(5);
        enemyPatrol.enabled = true;
        enemyAI.enabled = true;
    }
}
