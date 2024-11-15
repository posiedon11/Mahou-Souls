using Assets.Characters;
using Assets.Scripts.Characters.GothGirl;
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

namespace Assets.Scripts.Characters.GothGirl
{
    public class GothGirlStateMachine : CharacterStateMachine
    {
        [SerializeField] private GothGirlStateData stateData;



        public MoveStateMachine movementStateMachine;
        public ActionStateMachine actionStateMachine;

        public override void Awake()
        {
            base.Awake();


            movementStateMachine = new MoveStateMachine(this, stateData);
            actionStateMachine = new ActionStateMachine(this, stateData);

            stateMachines.Add(movementStateMachine);
            stateMachines.Add(actionStateMachine);

            InitializeStates();
            //InitializeStates();
        }

       

        protected override void InitializeStates()
        {
            if (DebugSettings.stateMachineTransations)

                Debug.Log("Initializing states");
            try
            {

                movementStateMachine.InitializeStates();
                actionStateMachine.InitializeStates();

                movementStateMachine.SetDefaultState();
                actionStateMachine.SetDefaultState();

            }
            catch (Exception e)
            {
                Debug.LogError($"Error initializing states: {e}");
            }
        }
        public override void Update()
        {
            base.Update();
               // movementStateMachine?.Update();
                //actionStateMachine?.Update();
                //currentState?.Update();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}
