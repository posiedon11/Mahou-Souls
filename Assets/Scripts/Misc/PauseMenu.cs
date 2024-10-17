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
        public void ContinueGame()
        {
            gameObject.SetActive(false);
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
}
