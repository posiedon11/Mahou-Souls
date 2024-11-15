using Assets.Characters.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Tools;
using UnityEditor;

namespace Assets.Scripts.Misc
{
    [Serializable]
    public class PlayerData
    {
        public string lastScene = "";
        public float maxHealth, maxStamina;
        public float currentHealth, currentStamina;

    }
    [Serializable]
    public class SaveData
    {
        public PlayerData playerData;
    }
    public class SaveManager
    {

        public PlayerData playerData;
        public SaveData saveData;

        public PlayerCharacter playerCharacter;

        private string saveFilePath;

       public SaveManager()
        {

            saveFilePath = $"{Application.persistentDataPath}/saveData.json";
            saveData = new SaveData();
            saveData.playerData = new PlayerData();

        }

        public void SaveGame()
        {
             SavePlayerData();

            string jsonData = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(saveFilePath, jsonData);

        }

        public SaveData LoadSave()
        {
           return LoadPlayerData();

        }
        private void SavePlayerData()
        {
            Debug.Log("saving player data");
            if (playerData == null)
            {
                playerData = new PlayerData();
            }
            if (playerCharacter != null)
            {
                //playerCharacter = playerObject.GetComponent<PlayerCharacter>();
                //playerCharacter = PlayerCharacter.instance;
                playerData.lastScene = SceneManager.GetActiveScene().name;
                Tools.Tools.CopyFields(playerCharacter, playerData);
                saveData.playerData = playerData;
            }        
        }
        private SaveData LoadPlayerData()
        {
            Debug.Log("loading player data");
            if (File.Exists(saveFilePath))
            {
                string jsonData = File.ReadAllText(saveFilePath);
                var saveData =   JsonUtility.FromJson<SaveData>(jsonData);
                return saveData;
                GameObject playerObject = GameObject.FindWithTag("Player");
                if (playerObject != null)
                {
                   // playerCharacter = PlayerCharacter.instance;

                    //playerCharacter = playerObject.GetComponent<PlayerCharacter>();
                    Debug.Log("Player data loaded.");
                }
                SceneManager.LoadSceneAsync(playerData.lastScene).completed += OnSceneLoaded;

            }
            return null;
        }
        private void OnSceneLoaded(AsyncOperation operation)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                //playerCharacter = playerObject.GetComponent<PlayerCharacter>();
                Tools.Tools.CopyFields(playerData, playerCharacter);  // Copy values from PlayerData to PlayerCharacter

                // Restore other fields as needed
                Debug.Log("Player data restored after scene load.");
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
