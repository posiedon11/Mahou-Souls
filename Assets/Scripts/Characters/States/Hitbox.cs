//hitbox is used to detect when a hitbox collides with an object
//generic
using Assets.Characters;
using Assets.Scripts.Characters.States;
using Assets.Scripts.Characters.States.Attacks;
using Assets.Scripts.Characters.States.Movement;
using Assets.Scripts.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField]
    private string hitboxName;
    private BaseState action;

    private BaseState<BaseStateData> nextState;
    private int targetLayer;
    public void Initialize(BaseState _action, string targetLayer = "Hitboxes" )
    { 
        action = _action;
        this.targetLayer = LayerMask.NameToLayer(targetLayer);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"{base.name} hit {collision.gameObject.name}");

        if (collision.gameObject.layer == targetLayer)
            action.OnHitboxTrigger(collision, base.name);
        //else
            //Debug.Log($"Hitbox hit {collision.gameObject.layer} layer, while aiming for {targetLayer}");
    }
}
