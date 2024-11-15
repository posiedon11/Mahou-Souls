//PlayerControls.cs
using Assets.Characters.Player;
using Assets.Scripts.Characters.States;
using Assets.Scripts.Characters.States.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
using Assets.Scripts.Characters.GothGirl;



public class PlayerControls : MonoBehaviour
{

    private PlayerCharacter playerCharacter;
    private GothGirlStateMachine stateMachine;

    

    private Vector2 moveDirection = Vector2.zero;
    //Initialize Controls
    private void Awake()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
        stateMachine = GetComponent<GothGirlStateMachine>();


        Debug.Log(stateMachine);

        //InitializeAttacks();
        //InitializeMovement();
        //stateMachine.SetDefualtState();
        Debug.Log($"PlayerControls awake for {playerCharacter.name}");
        

    }

    //Every Action that must be repeated every frame will be here
    private void FixedUpdate()
    {
        //walkAction.PerformAction();
    }
    private void Update()
    {
         //stateMachine.Update();
    }
    Vector2 moveValue;

    //public void OnMove(InputValue val)
    //{
    //    moveValue = val.Get<Vector2>();
    //    //animator.SetBool("Moving", moveValue.x != 0);
    //    if (moveValue.x > 0)
    //        playerCharacter.SetFacingRight(true);
    //    else if (moveValue.x < 0)
    //        playerCharacter.SetFacingRight(false);

    //}
    //The event tied to WASD to move the charracter
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            //Debug.Log("player moving");
            playerCharacter.moveDireciton = ctx.ReadValue<Vector2>();
            playerCharacter.SetFacingRight(playerCharacter.moveDireciton.x > 0);
            //moveDirection = ctx.ReadValue<Vector2>();
            //Debug.Log(moveDirection);
            //stateMachine.movementStateMachine.walkState.SetDirection(ctx.ReadValue<Vector2>());
            stateMachine.movementStateMachine.SetState(stateMachine.movementStateMachine.walkState);

        }
        else if (ctx.canceled)
        {
            playerCharacter.moveDireciton = Vector2.zero;
            //stateMachine.movementStateMachine.walkState.SetDirection(Vector2.zero);
            //stateMachine.SetState(stateMachine.idleState);
            //walkAction.StopAction();
        }
    }

    //The the event tied to the space bar to make the character jump
    public void OnJump(InputAction.CallbackContext ctx)
    {
        stateMachine.movementStateMachine.SetState(stateMachine.movementStateMachine.jumpState);
    }

    //The event tied to E to make the character attack
    public void OnLightAttack(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            stateMachine.actionStateMachine.startSwing = true;
            //stateMachine.actionStateMachine.SetState(stateMachine.actionStateMachine.overheadSwingState);

        }
    }
    public void OnHeavyAttack(InputAction.CallbackContext ctx)
    {
        Debug.Log("Heavy Attack");
    }

    //Initializes the movement actions
    //private void InitializeMovement()
    //{
    //    walkAction = GetComponent<WalkState>();
    //    if (walkAction != null)
    //    {
    //        Debug.Log($"{walkAction.actionName} found");
    //    }
    //    else
    //    {
    //        Debug.LogError("WalkAction not found");
    //    }
    //}
    ////Initializes the attack actions
    //private void InitializeAttacks()
    //{
    //    swordSwing = transform.Find("Attacks/SwordSwing").GetComponent<SwordSwingState>();

        
    //    if (swordSwing != null)
    //    {
    //        Debug.Log($"{swordSwing.actionName} found");
    //        swordSwing.Initialize(playerCharacter);
    //    }
    //    else
    //    {
    //        Debug.LogError("SwordSwing not found");
    //    }
    //}
    //private bool FoundAttack<T>(string transformPath) where T : BaseAttackState
    //{
    //    T attack = transform.Find(transformPath).GetComponent<T>();
    //    if (attack != null)
    //    {
    //        Debug.Log($"{attack.actionName} found");
            
    //        return true;
    //    }
    //    else
    //    {
    //        Debug.LogError($"{attack.actionName} not found");
    //        return false;
    //    }
    //}
}
