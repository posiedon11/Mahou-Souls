using Assets.Scripts.Characters.States.StateMachines;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters.States.Movement
{
    public class JumpState : GroundedState<GroundedStateData>
    {
        private JumpStateData jumpStateData;
        public JumpState(CharacterStateMachine _stateMachine, JumpStateData _stateData) : base(_stateMachine, _stateData)
        {
            jumpStateData = _stateData;
            canInterrupt = false;
        }
        public override void OnEnter()
        {
            if (!character.IsGrounded())
            {
                return;
            }
            base.OnEnter();
            // animator.Play("MagicalGirl_Jump");
            rb.AddForce(Vector2.up * jumpStateData.baseJumpForce, ForceMode2D.Impulse);
        }

        public override bool CanPreform()
        {
            return base.CanPreform();
        }
    }
}
