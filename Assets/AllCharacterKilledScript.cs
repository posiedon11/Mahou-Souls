using Assets.Characters;
using Assets.Characters.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManagerTest : MonoBehaviour
{
    [SerializeField]
    public  List<BaseCharacter> enemies;

    public PlayerCharacter player;

    public Canvas canvas;

    // Start is called before the first frame update

    private void Awake()
    {
        if (canvas != null)
        {
            // gameObject.transform.Find("StageCanvas");
            canvas.gameObject.SetActive(false);
        }
    }
    void Start()
    {
        if (enemies == null)
        {
            enemies = enemies = new List<BaseCharacter>(FindObjectsOfType<BaseCharacter>());
        }

        foreach (BaseEnemy enemy in enemies)
        {
            enemy.OnCharacterDeath += OnEnemyDestroyed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnEnemyDestroyed(BaseCharacter enemy)
    {
        Debug.Log("Enemy Destroyed from event");
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            if(enemies.Count == 0)
            {
                Debug.Log("All enemies killed");
                StageOver();
            }
        }
    }

    public void StageOver()
    {
        Debug.Log("Stage Over");
        canvas.gameObject.SetActive(true);
    }

}
