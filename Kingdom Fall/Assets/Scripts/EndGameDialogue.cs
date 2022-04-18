using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameDialogue : MonoBehaviour
{
    public Text endText;
    public GameObject panel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            panel.SetActive(true);
            endText.enabled = true;
            endText.text = "Press 'E' to end the game";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                endText.enabled = false;
                panel.SetActive(false);
            }

        }

    }
}
