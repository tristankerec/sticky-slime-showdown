using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene("mainScene");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("howToPlay");
    }

    public void Menu ()
    {
        SceneManager.LoadScene("Menu");
    }
}
