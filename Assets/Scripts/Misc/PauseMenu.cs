using Assets.Scripts.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        public SaveManager saveManager;
        public GameManager gameManager;
        public void Awake()
        {
            saveManager = new SaveManager();
        }
        public void Start()
        {
            gameManager = GameManager.instance;
        }
        public void ContinueGame()
        {
            Time.timeScale = 1;

            gameObject.SetActive(false);
        }

        public void PauseGame()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        public void LoadScene(string sceneName = "")
        {
            Time.timeScale = 1;

            UnityEngine.Debug.Log($"{sceneName} is loading");

            SceneManager.LoadScene(sceneName);
        }

        public void SaveGame()
        {
            gameManager.SaveGame();
           //saveManager.SaveGame();
        }
        public void LoadGame()
        {
            gameManager.LoadGame();
            //saveManager.LoadSave();
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
