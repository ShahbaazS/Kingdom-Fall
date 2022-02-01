using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPossession : MonoBehaviour
{
    // For detecting enemies
    [SerializeField] LayerMask enemyLayer;

    // The point where the player can possess an enemy
    [SerializeField] Transform possessPoint;

    // possession parameters
    float possessRadius = 0.25f;
    float possessCooldown = 10f;
    float nextPossessTime = 0f;

    void Update()
    {
        // makes sure that the cooldown is over before the player can possess
        if(Time.time >= nextPossessTime)
        {
            // on pressing "x" (can be changed later), you can possess enemies
            if (Input.GetKeyDown(KeyCode.X))
            {
                Possess();
                nextPossessTime = Time.time + possessCooldown; // adds cooldown to the current time
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
            hitEnemies[0].GetComponent<PlayerControl>().TakeOver();
        }
    }

}
