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
    public class IdleState: GroundedState<IdleStateData>
    {
        public IdleState(CharacterStateMachine _stateMachine, IdleStateData _stateData) : base(_stateMachine, _stateData)
        {
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnEnter()
        {
            base.OnEnter();
           // animator.Play("MagicalGirl_Idle");
        }
        public override void Update()
        {
            base.Update();
            rb.velocity = new Vector2( Mathf.MoveTowards(rb.velocity.x, 0, 1 * Time.deltaTime),rb.velocity.y);
        }
    } 
}
