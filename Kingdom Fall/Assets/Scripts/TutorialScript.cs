using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Text tutorialText;
    public int index = 0;
    string[] texts = { "Press 'A' or 'D' to move left or right",
                       "Press spacebar to jump",
                       "Press 'x' to possess the enemy", 
                       "Spam left-click to win the struggle", 
                       "Left-click to shoot", 
                       "Right-click to use special ability",
                       "Kill the enemy archer",
                       "Press 'x' to exit and kill the body",
                       "Pass through the gate"
    };

    PlayerControl playerControl;
    public GameObject enemy;
    public GameObject end;

    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        index = 0;
        //tutorialText.enabled = false;
    }

    void Update()
    {
        switch (index) {
            case 0:
                tutorialText.text = texts[index];
                if (Input.GetAxisRaw("Horizontal") != 0)
                    index++;
                break;
            case 1:
                tutorialText.text = texts[index];
                if (Input.GetKeyDown(KeyCode.Space))
                    index++;
                break;
            case 2:
                tutorialText.text = texts[index];
                if (playerControl.resistStarted)
                    index++;
                break;
            case 3:
                tutorialText.text = texts[index];
                if (playerControl.isPossessed)
                    index++;
                else if (!playerControl.resistStarted)
                    index--;
                break;
            case 4:
                tutorialText.text = texts[index];
                if (Input.GetButtonDown("Fire1"))
                    index++;
                break;
            case 5:
                tutorialText.text = texts[index];
                if (Input.GetButtonDown("Fire2"))
                    index++;
                break;
            case 6:
                tutorialText.text = texts[index];
                if (enemy.GetComponent<Health>().health <= 0)
                    index++;
                break;
            case 7:
                tutorialText.text = texts[index];
                if (!playerControl.isPossessed)
                    index++;
                break;
            case 8:
                tutorialText.text = texts[index];
                if (end.GetComponent<TutorialEnd>().tutorialEnd)
                    tutorialText.enabled = false;
                break;
            default:
                index = 0;
                break;
        }
    }
}
