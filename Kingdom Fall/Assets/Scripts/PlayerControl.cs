using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] GameObject entity; // player

    public bool isPossessed = false;

    public void TakeOver()
    {
        // disables player
        entity.SetActive(false);

        // activates the player movement script on the enemy
        GetComponent<PlayerMovement>().enabled = true;
        virtualCamera.Follow = transform; // changes the camera target

        isPossessed = true;
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
    }
}
