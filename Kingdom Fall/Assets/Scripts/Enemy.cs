using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    // reference to player control script
    PlayerControl playerControl;

    TutorialScript tutorialScript;

    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        tutorialScript = GetComponent<TutorialScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (playerControl.isPossessed)
            {
                if(tutorialScript && tutorialScript.index == 7)
                {
                    playerControl.Eject();
                    StartCoroutine(Die());
                }
                else if(tutorialScript == null){
                    playerControl.Eject();
                    StartCoroutine(Die());
                }
                
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
        if(GetComponent<EnemyPatrol>())
            GetComponent<EnemyPatrol>().enabled = false;
        if (GetComponent<EnemyAI>())
            GetComponent<EnemyAI>().enabled = false;
        GetComponent<Health>().enabled = false;
        transform.Find("HealthUI").gameObject.SetActive(false);
        transform.Find("Model").gameObject.SetActive(false);

        // waits until cooldown is over
        yield return new WaitForSeconds(10f);
        Destroy(gameObject); // destroys
    }
}
