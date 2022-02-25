using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerControl : MonoBehaviour
{
    //todo coroutines for ui and struggle

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] GameObject entity; // player

    public float possessCooldown = 10f;
    float nextPossessTime = 0f;

    public bool isPossessed = false;

    public void TakeOver()
    {
        // makes sure that the cooldown is over before the player can possess
        if (Time.time >= nextPossessTime)
        {
            // disables player
            entity.SetActive(false);

            // activates the player movement script on the enemy
            GetComponent<PlayerMovement>().enabled = true;
            virtualCamera.Follow = transform; // changes the camera target

            isPossessed = true;
            nextPossessTime = Time.time + possessCooldown; // adds cooldown to the current time
        }

    }

    public void Eject()
    {
        // sets player position to the enemy position
        entity.transform.position = transform.position;

        // enables player
        entity.SetActive(true);

        // deactivates the player movement script on the enemy
        GetComponent<PlayerMovement>().enabled = false;
        virtualCamera.Follow = entity.transform; // changes the camera target

        isPossessed = false;
        nextPossessTime = Time.time + possessCooldown; // adds cooldown to the current time
    }
}
