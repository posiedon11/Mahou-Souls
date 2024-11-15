//Base Enemy will be used for all enemy types
using Assets.Characters;
using Assets.Scripts.Characters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : BaseCharacter
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject healthBarObject;

    private StatusBar healthBar;


    //default faction is enemy
    protected override void Start()
    {
        base.Start();
        faction = CharacterFaction.Enemy;
        healthBar = new StatusBar(healthBarObject);

    }

    // Update is called once per frame
    //Enemy health bar will be updated every frame
    public override void Update()
    {
        base.Update();
        
        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        healthBar.SetFill(currentHealth / maxHealth);
        healthBar.SetText("");
    }

    //Enemy will die when health reaches 0, and destroy the game object
    protected override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
