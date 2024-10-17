using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransation : MonoBehaviour
{

    //public SceneAsset nextScene; f
    public string nextScene;
    // Start is called before the first frame update
    public void LoadScene(string sceneName = "")
    {
        UnityEngine.Debug.Log($"{sceneName} is loading");

        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //UnityEngine.Debug.Log($"{sceneName} is loading");

           
            SceneManager.LoadScene(nextScene);
        }
    }
}
