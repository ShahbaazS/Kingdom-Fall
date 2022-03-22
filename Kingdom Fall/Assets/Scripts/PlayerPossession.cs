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

    public float possessCooldown = 10f;
    public float nextPossessTime = 0f;

    // cooldown ui stuff
    public Image icon;
    public bool isCooldown = false;


    Collider2D currentEnemy;

    void Update()
    {
        // on pressing "x" (can be changed later), you can possess enemies
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Time.time > nextPossessTime)
                Possess();
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
            currentEnemy.GetComponent<PlayerControl>().StartPossession();
        }
    }

    public IEnumerator Cooldown()
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

    public void AddNextPossessTime()
    {
        nextPossessTime = Time.time + possessCooldown; // adds cooldown to the current time
    }

}
