using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Misc
{
    public class BaseMenu : MonoBehaviour
    {
        public string previousMenu;
        public string nextMenu;
        public void LoadScene(string sceneName = "")
        {
            UnityEngine.Debug.Log($"{sceneName} is loading");

            SceneManager.LoadScene(sceneName);
        }

        public void GoBack()
        {

        }
    }
}
