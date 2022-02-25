using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // reference to player control script
    PlayerControl playerControl;

    // cooldown ui stuff
    public Image icon;
    bool isCooldown = false;


    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        icon.fillAmount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            // makes sure enemy is possessed before ejecting player
            if (playerControl.isPossessed)
            {
                playerControl.Eject();
                isCooldown = true;
                icon.fillAmount = 1;
            }
        }
        // cooldown ui
        if (isCooldown)
        {
            icon.fillAmount -= 1 / playerControl.possessCooldown * Time.deltaTime;

            if (icon.fillAmount <= 0)
            {
                icon.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
