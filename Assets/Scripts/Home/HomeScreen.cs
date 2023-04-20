using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("MatchingGameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToHomeScreen()
    {
        SceneManager.LoadScene("HomeScreen");
    }

}
