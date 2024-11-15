using Assets.Scripts.Characters.States.Attacks.Melee;
using Assets.Scripts.Characters.States.Movement;
using Assets.Scripts.Characters.States;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Characters.States.StateMachines;
using UnityEngine;
using Assets.Scripts.Characters.GothGirl;

namespace Assets.Scripts.Characters.Enemies.StateMachines
{
    public class MovementStateMachine : StateMachine
    {
        protected CharacterStateData stateData;

        public IdleState idleState;
        public WalkState walkState;
        public JumpState jumpState;
        public FallState fallState;
        public MovementStateMachine(GothGirlStateMachine _stateMachine, CharacterStateData _stateData) : base(_stateMachine)
        {
            stateData = _stateData;
            //InitializeStates();
        }

        public override void InitializeStates()
        {
            if (DebugSettings.stateMachineTransations)

                Debug.Log("Initializing states");
            try
            {

                idleState = new IdleState(parentMachine, stateData.idleStateData);
                walkState = new WalkState(parentMachine, stateData.walkStateData);
                jumpState = new JumpState(parentMachine, stateData.jumpStateData);
                fallState = new FallState(parentMachine, stateData.fallStateData);

                //Debug.Log($"Idle State:   {idleState}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error initializing states: {e}");
            }
        }
        public override void SetDefaultState()
        {
            SetState(idleState);
        }
        public override bool ValidState(BaseState? stateData)
        {
            //Debug.Log(stateData);
            switch (stateData)
            {
                case IdleState:
                    return true;
                case WalkState _:
                    return true;
                case JumpState _:
                    return true;
                case FallState _:
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

        public OverheadSwingState overheadSwingState;

        public EmptyState emptyState;
        public ActionStateMachine(GothGirlStateMachine _stateMachine, CharacterStateData _stateData) : base(_stateMachine)
        {
            stateData = _stateData;
            //InitializeStates();
        }

        public override void InitializeStates()
        {
            if (DebugSettings.stateMachineTransations)
                Debug.Log("Initializing states");
            try
            {
                overheadSwingState = new OverheadSwingState(parentMachine, stateData.overHeadSwingData);
                emptyState = new EmptyState(parentMachine, stateData.emptyStateData);
                //overheadSwingState.Initialize();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error initializing states: {e}");
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
                case OverheadSwingState _:
                    return true;
                case null:
                    return true;
                default:
                    return false;
            }
        }
    }
}
