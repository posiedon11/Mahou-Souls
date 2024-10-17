using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using Assets.Characters.Player;

namespace Assets.Scripts.Misc
{
    public class SceneLoadManager : MonoBehaviour
    {
        public static SceneLoadManager instance;

        public PlayerCharacter playerCharacter;
        public string currentScene;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            //SceneManager.sceneLoaded += OnSceneLoaded;
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("Scene loaded");
            GameObject playerObject = GameObject.Find("GothGirlPlayer");
            if (playerObject != null)
            {
                Debug.Log("Player object found");
               playerCharacter = playerObject.GetComponent<PlayerCharacter>();
                SaveManager.instance.playerCharacter = playerCharacter;
            }
            if (SaveManager.instance.HasSaveFile())
            {
                SaveManager.instance.playerCharacter = playerCharacter;
                currentScene = SceneManager.GetActiveScene().name;
                SaveManager.instance.LoadSave();
                PlayerData playerData = SaveManager.instance.playerData;

                if (playerData.lastScene == SceneManager.GetActiveScene().name)
                {
                    SceneManager.LoadScene(playerData.lastScene);
                }
            }

                SaveManager.instance.SaveGame();
            
        }

        public bool LoadScene()
        {
            if (SaveManager.instance.HasSaveFile())
            {
                SaveManager.instance.LoadSave();
                PlayerData playerData = SaveManager.instance.playerData;
                if (playerData != null && playerData.lastScene != null && playerData.lastScene != string.Empty)
                {
                    Debug.Log($"Loading scene {playerData.lastScene}");
                    SceneManager.LoadScene(playerData.lastScene);
                    return true;
                }
            }
            return false;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

    }
}
