using Assets.Scripts.Characters.States.StateMachines;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.States.Movement
{
    public class FallState : BaseState<FallStateData>
    {
        public FallState(CharacterStateMachine _stateMachine, FallStateData _stateData) : base(_stateMachine, _stateData)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            //animator.Play("MagicalGirl_Fall");
        }

        public override bool CanExit()
        {
            return base.CanExit() && character.IsGrounded();
        }
    }
}
