using Assets.Characters;
using Assets.Scripts.Characters.States;
using Assets.Scripts.Characters.States.Attacks.Melee;
using Assets.Scripts.Characters.States.Movement;
using Assets.Scripts.Characters.States.StateMachines;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
#nullable enable
namespace Assets.Scripts.Characters.GothGirl
{
    public class MoveStateMachine : StateMachine
    {
        protected GothGirlStateData stateData;
        public BaseCharacter character;


        public IdleState? idleState = null;
        public WalkState? walkState = null;
        public JumpState? jumpState = null;
        public FallState? fallState = null;
        public LandingState? landingState = null;


        public bool isSprinting = false;
        public bool isWalking = false;

        public MoveStateMachine(GothGirlStateMachine _stateMachine, GothGirlStateData _stateData) : base(_stateMachine)
        {
            stateData = _stateData;
            character = parentMachine.character;
            //InitializeStates();
        }

        public override void InitializeStates()
        {
            if (DebugSettings.stateMachineTransations)

                Debug.Log("Initializing states for MoveMentStateMachine");
            try
            {

                idleState = new IdleState(parentMachine, stateData.idleStateData);
                walkState = new WalkState(parentMachine, stateData.walkStateData);
                jumpState = new JumpState(parentMachine, stateData.jumpStateData);
                fallState = new FallState(parentMachine, stateData.fallStateData);
                landingState = new LandingState(parentMachine, stateData.landStateData);

                //walkState.SetDirection(moveDirection);

                //Debug.Log($"Idle State:   {idleState}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error initializing states: {e}");
            }
        }

        public override void Update()
        {
            base.Update();
            //Debug.Log(character.moveDireciton);
            if (character.moveDireciton == Vector2.zero && character.rb.velocity.x == 0)
            {
                // Debug.Log("Not moving");
                isWalking = false;
            }
            else
                isWalking = true;

            if (!character.grounded && currentState != fallState)
            {
                SetState(fallState);
            }
            switch (currentState)
            {
                case WalkState:
                    if (!isWalking)
                        SetState(idleState);
                    break;


                case JumpState:
                    SetState(fallState);
                    break;


                case FallState:
                    if (parentMachine.character.grounded)
                        SetState(landingState);
                    break;


                case LandingState _:
                    if (isWalking)
                        SetState(walkState);
                    else
                        SetState(idleState);
                    break;


                case IdleState:
                    if (isWalking)
                        SetState(walkState);
                    break;
                case null:
                    break;
            }
        }
        public override void SetDefaultState()
        {
            SetState(idleState);
        }
        public override bool ValidState(BaseState? stateData)
        {
            // Debug.Log(moveDirection);
            //Debug.Log(stateData);
            switch (stateData)
            {
                case IdleState:
                    return true;
                case WalkState:
                    return true;
                case JumpState:
                    return true;
                case FallState:
                    return true;
                case LandingState:
                    return true;
                case null:
                    return false;
                default:
                    return false;
            }
        }
    }

    public class ActionStateMachine : StateMachine
    {
        protected CharacterStateData stateData;

        public OverheadSwingState? overheadSwingState;

        public EmptyState? emptyState = null;
        public ActionStateMachine(GothGirlStateMachine _stateMachine, CharacterStateData _stateData) : base(_stateMachine)
        {
            stateData = _stateData;
            //InitializeStates();
        }

        public override void InitializeStates()
        {
            if (DebugSettings.stateMachineTransations)
                Debug.Log("Initializing states for Action State Machine");
            try
            {
                overheadSwingState = new OverheadSwingState(parentMachine, stateData.overHeadSwingData);
                //overheadSwingState.Initialize();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error initializing states: {e}");
            }
        }
        public override void Update()
        {
            base.Update();
            switch (currentState)
            {
                case OverheadSwingState:
                    if (currentState.CanExit())
                    {
                        SetState1(emptyState);
                        //currentState?.OnExit();
                        //currentState = null;
                    }
                    break;
                case null:
                    break;
            }
        }
        public override void SetDefaultState()
        {
            currentState = emptyState;
        }
        public override bool ValidState(BaseState? stateData)
        {
            switch (stateData)
            {
                case OverheadSwingState:
                    return true;
                case null:
                    return true;
                default:
                    return false;
            }
        }
    }
}
