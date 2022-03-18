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
            gameObject.tag = "Player";
            entity.tag = "Untagged";

            enemyAI.enabled = false;
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
        switch (activeEnemy) 
        {
            case "Knight":
                knightWeapon.enabled = true;
                break;
            case "Archer":
                archerWeapon.enabled = true;
                break;
            case "Mage":
                mageWeapon.enabled = true;
                break;
            default:
                break;
        }

        initialRotation = transform.rotation;
        struggleUI.SetActive(false);

        GetComponent<PlayerMovement>().facingRight = enemyPatrol.isFacingRight();

        // activates the player movement script on the enemy
        GetComponent<PlayerMovement>().enabled = true;
        virtualCamera.Follow = transform; // changes the camera target

        entity.GetComponent<PlayerPossession>().nextPossessTime = Time.time + entity.GetComponent<PlayerPossession>().possessCooldown; // adds cooldown to the current time

        entity.GetComponent<PlayerPossession>().isCooldown = true;
        StartCoroutine(entity.GetComponent<PlayerPossession>().Cooldown());
    }

    public void Eject()
    {
        gameObject.tag = "Untagged";
        entity.tag = "Player";

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

        StartCoroutine(ActivateEnemyAI());

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
                resistStarted = false;

                resist = 5;
                Eject();
                entity.GetComponent<PlayerPossession>().nextPossessTime = Time.time + entity.GetComponent<PlayerPossession>().possessCooldown; // adds cooldown to the current time

                entity.GetComponent<PlayerPossession>().isCooldown = true;
                StartCoroutine(entity.GetComponent<PlayerPossession>().Cooldown());
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
