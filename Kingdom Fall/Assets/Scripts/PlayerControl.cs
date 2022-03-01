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
    Weapon weapon;

    // cooldown ui stuff
    public Image icon;
    bool isCooldown = false;

    // cooldown parameters
    public float possessCooldown = 10f;
    float nextPossessTime = 0f;

    public bool isPossessed = false;

    //resist parameters
    public bool resistStarted = false;

    float resistIncrement = 1f;
    float resistAmount = 2f;
    float resist = 5f;
    float possessAmount = 10f;
    float decreaseAmount = 0f;

    // for reseting rotation back to when not possessed
    Quaternion initialRotation;

    public void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        weapon = GetComponent<Weapon>();
        struggleBar = struggleUI.GetComponentInChildren<StruggleBar>();
        icon.fillAmount = 0;
        struggleUI.SetActive(false);
    }

    public void StartPossession()
    {
        // makes sure that the cooldown is over before the player can possess
        if (Time.time >= nextPossessTime)
        {
            gameObject.tag = "Player";
            entity.tag = "Untagged";

            struggleUI.SetActive(true);
            struggleBar.SetMaxStruggle(possessAmount);

            // disables player
            entity.GetComponent<PlayerMovement>().enabled = false;
            entity.SetActive(false);

            resistStarted = true;
            StartCoroutine(Resist());
        }
    }

    void TakeOver()
    {
        weapon.enabled = true;

        enemyAI.enabled = false;
        enemyPatrol.enabled = false;

        initialRotation = transform.rotation;
        struggleUI.SetActive(false);

        GetComponent<PlayerMovement>().facingRight = enemyPatrol.isFacingRight();

        // activates the player movement script on the enemy
        GetComponent<PlayerMovement>().enabled = true;
        virtualCamera.Follow = transform; // changes the camera target
        nextPossessTime = Time.time + possessCooldown; // adds cooldown to the current time
    }

    public void Eject()
    {
        gameObject.tag = "Untagged";
        entity.tag = "Player";

        weapon.enabled = false;

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
        nextPossessTime = Time.time + possessCooldown; // adds cooldown to the current time

        isCooldown = true;
        icon.fillAmount = 1;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
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
    }

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
            if (resist <= 0)
            {
                isPossessed = false;
                resistStarted = false;

                resist = 5;
                Eject();
            }

            yield return null;    // waits one frame
        }
    }

    IEnumerator ActivateEnemyAI()
    {
        yield return new WaitForSeconds(1);
        enemyPatrol.enabled = true;
        enemyAI.enabled = true;
    }
}
