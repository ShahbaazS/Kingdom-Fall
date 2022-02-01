using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] GameObject entity; // player

    public void TakeOver()
    {
        /*entity.GetComponent<PlayerMovement>().enabled = false;*/

        // disables player
        entity.SetActive(false);

        // activates the player movement script on the enemy
        GetComponent<PlayerMovement>().enabled = true;
        virtualCamera.Follow = transform; // changes the camera target
    }
}
