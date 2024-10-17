//Mainmenu.cs
//the games main menu
using Assets.Scripts.Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public SceneLoadManager sceneLoadManager;

    public void Awake()
    {
        sceneLoadManager = SceneLoadManager.instance;
    }
    public void StartGame()
    {
        UnityEngine.Debug.Log("Game is starting");
        SceneManager.LoadScene("Game");
    }
    public void ContineuGame(string sceneName="")
    {
        if(SceneLoadManager.instance.LoadScene())
        {
            if (SaveManager.instance.HasSaveFile())
                sceneLoadManager.LoadScene();
            else LoadScene(sceneName);
        }
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
