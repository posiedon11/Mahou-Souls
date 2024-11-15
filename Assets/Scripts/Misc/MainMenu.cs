//Mainmenu.cs
//the games main menu
using Assets.Scripts;
using Assets.Scripts.Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        UnityEngine.Debug.Log("Game is starting");
        GameManager.instance.ResetGame();
        LoadScene("IntroScene");
    }
    public void ContineuGame()
    {
        GameManager.instance.LoadGame();
    }
    public void LoadScene(string sceneName = "")
    {
        UnityEngine.Debug.Log($"{sceneName} is loading");

        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
