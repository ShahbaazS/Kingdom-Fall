using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{

    public string TryAgain;
    public string MainMenu;
    // Start is called before the first frame update
    public void Setup(){
        gameObject.SetActive(true);
    }
    
    public void TryAgainButton(){
        SceneManager.LoadScene(TryAgain);
    }

    public void MainMenuButton(){
        SceneManager.LoadScene(MainMenu);
    }

}
