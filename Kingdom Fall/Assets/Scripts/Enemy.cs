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
            // makes sure enemy is possessed before ejecting player
            if (playerControl.isPossessed)
            {
                playerControl.Eject();
            }
        }
    }
}
