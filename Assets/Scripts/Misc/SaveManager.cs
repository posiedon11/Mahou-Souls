using Assets.Characters.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Misc
{
    [Serializable]
    public class PlayerData
    {
        public string lastScene = "";
        public float playerHealth = 100;

    }
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager instance;

        public PlayerData playerData;

        public PlayerCharacter playerCharacter;

        private string saveFilePath;

        private void Awake()
        {
            saveFilePath = Application.persistentDataPath + "/playerSaveData.json";
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

        public void SaveGame()
        {
            SavePlayerData();
        }

        public void LoadSave()
        {
            LoadPlayerData();
        }
        private void SavePlayerData()
        {
            Debug.Log("saving player data");
            if (playerData == null)
            {
                playerData = new PlayerData();
            }
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                playerCharacter = playerObject.GetComponent<PlayerCharacter>();
                playerData.lastScene = SceneManager.GetActiveScene().name;
                playerData.playerHealth = playerCharacter.currentHealth;
                string jsonData = JsonUtility.ToJson(playerData, true);
                File.WriteAllText(saveFilePath, jsonData);
            }        
        }
        private void LoadPlayerData()
        {
            Debug.Log("loading player data");
            if (File.Exists(saveFilePath))
            {
                GameObject playerObject = GameObject.Find("GothGirlPlayer");
                if (playerObject != null)
                {
                    //playerCharacter = playerObject.GetComponent<PlayerCharacter>();
                    //string jsonData = File.ReadAllText(saveFilePath);
                    //playerData = JsonUtility.FromJson<PlayerData>(jsonData);
                    //playerCharacter.currentHealth = playerData.playerHealth;

                }
            }
        }

        public bool HasSaveFile()
        {
            return System.IO.File.Exists(saveFilePath);
        }

        public void ResetSave()
        {
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
            }
            playerData = new PlayerData();
        }
    }

}
