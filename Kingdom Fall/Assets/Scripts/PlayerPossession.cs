using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPossession : MonoBehaviour
{
    // For detecting enemies
    [SerializeField] LayerMask enemyLayer;

    // The point where the player can possess an enemy
    [SerializeField] Transform possessPoint;

    // possession parameters
    float possessRadius = 0.25f;
    float possessCooldown = 10f;
    public float nextPossessTime = 0f;

    Collider2D currentEnemy;

    // cooldown ui stuff
    public Image icon;
    bool isCooldown = false;

    private void Start()
    {
        icon.fillAmount = 0;
    }

    void Update()
    {
        // makes sure that the cooldown is over before the player can possess
        if(Time.time >= nextPossessTime)
        {
            // on pressing "x" (can be changed later), you can possess enemies
            if (Input.GetKeyDown(KeyCode.X))
            {
                Possess();

                isCooldown = true;
                icon.fillAmount = 1;

                nextPossessTime = Time.time + possessCooldown; // adds cooldown to the current time
            }
        }
        
        // cooldown ui
        if (isCooldown)
        {
            icon.fillAmount -= 1 / possessCooldown * Time.deltaTime;

            if (icon.fillAmount <= 0)
            {
                icon.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    void Possess()
    {
        // an array of enemies that are hit by the possession
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(possessPoint.position, possessRadius, enemyLayer);

        if (hitEnemies.Length == 0)
        {
            return;
        }
        else
        {
            // takes the first enemy to be hit and lets the player take control
            currentEnemy = hitEnemies[0];
            currentEnemy.GetComponent<PlayerControl>().TakeOver();
        }
    }

}
