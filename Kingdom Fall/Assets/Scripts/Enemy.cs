using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    // reference to player control script
    PlayerControl playerControl;

    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (playerControl.isPossessed)
            {
                playerControl.Eject();
            }
        }
    }

    public IEnumerator Die()
    {
        if (playerControl.isPossessed)
        {
            playerControl.Eject();
        }
        else if (playerControl.resistStarted)
        {
            playerControl.resistStarted = false;
            playerControl.Eject();
        }

        // disables everything except Enemy script and PlayerControlScript
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach(Collider2D collider in colliders){
            collider.enabled = false;
        }
        GetComponent<Renderer>().enabled = false;
        GetComponent<EnemyPatrol>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<Health>().enabled = false;
        transform.Find("HealthUI").gameObject.SetActive(false);
        transform.Find("Model").gameObject.SetActive(false);

        // waits until cooldown is over
        yield return new WaitForSeconds(10f);
        Destroy(gameObject); // destroys
    }
}
