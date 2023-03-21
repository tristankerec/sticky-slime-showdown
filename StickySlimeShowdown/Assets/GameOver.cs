using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene(1);
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
