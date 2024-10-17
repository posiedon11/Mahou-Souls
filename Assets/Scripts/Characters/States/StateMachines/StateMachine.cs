using Assets.Scripts.Characters.States.StateMachines;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

#nullable enable
namespace Assets.Scripts.Characters.States.StateMachines
{
    public abstract class StateMachine
    {
        protected BaseState? currentState;
        protected CharacterStateMachine parentMachine;

        public StateMachine(CharacterStateMachine _stateMachine)
        {
            //Debug.Log("parent ", parentMachine);
            parentMachine = _stateMachine;
            //InitializeStates();
            //SetDefaultState();
        }

        public virtual void SetState1(BaseState? newState = null)
        {
            if (DebugSettings.stateMachineTransations)
            {
                Debug.Log("Attempting to set State");
                Debug.Log($"{currentState}  ->  {newState}");
            }
            if (ValidState(newState))
            {
                if (currentState != null && !currentState.CanExit())
                {
                    return;
                }
                else if (newState != null && !newState.CanPreform()) return;
                currentState?.OnExit();
                currentState = newState;
                currentState?.OnEnter();
            }
            else
            {
                if (newState != null)
                Debug.Log($"Invalid state for { newState.actionName}");
                else
                    Debug.Log("Invalid state, null");
            }
        }
        public virtual void SetState<TStateData>(BaseState<TStateData>? newState = null) where TStateData : BaseStateData
        {
            SetState1(newState);
            //if (DebugSettings.stateMachineTransations)
            //{
            //    Debug.Log("Attempting to set State");
            //    Debug.Log($"{currentState}  ->  {newState}");
            //}
            //if (newState != null && ValidState(newState))
            //{
            //    if (currentState != null && !currentState.CanExit())
            //    {
            //        return;
            //    }
            //    currentState?.OnExit();
            //    currentState = newState;
            //    currentState?.OnEnter();
            //}
            //else
            //{
            //    Debug.Log("Invalid state");
            //}
        }

        public virtual void Update()
        {
            if (currentState != null)
                currentState?.Update();
        }
        public abstract void InitializeStates();
        public abstract void SetDefaultState();

        public virtual void SetNextState() { }

        public abstract bool ValidState(BaseState? stateData);
    }
}
