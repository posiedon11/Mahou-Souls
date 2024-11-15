using Assets.Characters.Player;
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
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public PlayerCharacter playerCharacter;
        public PlayerCharacter playerPrefab;
        public SaveManager saveManager;

        private SaveData saveData;
        private bool loadGame = false;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                saveManager = new SaveManager();
                SceneManager.sceneLoaded += OnSceneLoaded;

            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

        }
        public void SaveGame()
        {
            GetPlayer();

            saveManager.SaveGame();
        }
        public void LoadGame()
        {
            GetPlayer();
            var loadedData = saveManager.LoadSave();

            loadGame = true;
            if (loadedData != null)
            {
                saveData = loadedData;
                SceneManager.LoadSceneAsync(saveData.playerData.lastScene);
            }
        }
        private void GetPlayer()
        {
            if (playerCharacter == null)
            {
                GameObject playerObject = GameObject.FindWithTag("Player");
                if (playerObject != null)
                {
                    playerCharacter = playerObject.GetComponent<PlayerCharacter>();
                    saveManager.playerCharacter = playerCharacter;
                }
            }
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "MainMenu")
            {
                return;
            }
            SpawnOrAssignMainPlayerCharacter();


            if (loadGame && saveData != null)
            {
                Tools.Tools.CopyFields(saveData.playerData, playerCharacter);
                loadGame = false;
            }
            Camera.main.GetComponent<CameraFollow>().SetTarget(playerCharacter.transform);

        }
        private void SpawnOrAssignMainPlayerCharacter()
        {
            // Check if main player character already exists
            if (playerCharacter == null)
            {
                // Try to find any player character in the scene (for testing)
                PlayerCharacter scenePlayerCharacter = FindObjectOfType<PlayerCharacter>();

                if (scenePlayerCharacter != null)
                {
                    // Set the found player character as the main one
                    playerCharacter = scenePlayerCharacter;
                }
                else
                {
                    // No player character in the scene, so spawn the main character from prefab
                    playerCharacter = Instantiate(playerPrefab);
                }
                saveManager.playerCharacter = playerCharacter;
                DontDestroyOnLoad(playerCharacter.gameObject);  // Make main player character persistent
            }
            else
            {
                // If main player character already exists, destroy any other player characters
                foreach (var scenePlayer in FindObjectsOfType<PlayerCharacter>())
                {
                    if (scenePlayer != playerCharacter)
                    {
                        Destroy(scenePlayer.gameObject);  // Remove duplicate player characters
                    }
                }

                // Move main player character to the correct spawn point in the new scene
                PositionPlayerAtSpawnPoint();
            }
            Camera.main.GetComponent<CameraFollow>().SetTarget(playerCharacter.transform);

        }
        private void PositionPlayerAtSpawnPoint(string spawnString = "DefaultSpawn")
        {
            GameObject spawnPoint = GameObject.Find(spawnString);
            if  (spawnPoint != null && playerCharacter != null)
            {
                playerCharacter.transform.position = spawnPoint.transform.position;
            }
            else
            {
                Debug.LogWarning("Spawn point not found or player character not set in GameManager");
            }
        }

        public void ResetGame()
        {
            foreach (var scenePlayer in FindObjectsOfType<PlayerCharacter>())
            {

                    Destroy(scenePlayer.gameObject);  // Remove duplicate player characters
            }
            saveData = null;
        }
    }
}
