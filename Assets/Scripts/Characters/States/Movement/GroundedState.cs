using Assets.Scripts.Characters.States.StateMachines;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.States.Movement
{
    public class GroundedState<TGroundedData> : BaseState<TGroundedData> where TGroundedData: GroundedStateData
    {
        public GroundedState(CharacterStateMachine _stateMachine, TGroundedData _stateData) : base(_stateMachine, _stateData)
        {
        }
        public override bool CanAffordResources()
        {
            if (!character.IsGrounded())
            {
                return false;
            }
            return base.CanAffordResources();
        }
        bool isGrounded = false;

    } 
}
