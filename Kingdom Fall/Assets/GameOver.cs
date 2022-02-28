using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public string retry;
    public string MainMenu;

    public void Setup(){
        gameObject.SetActive(true);
    }

    public void RetryButton(){
        SceneManager.LoadScene(retry);
    }

    public void MainMenuButton(){
        SceneManager.LoadScene(MainMenu);
    }

}
