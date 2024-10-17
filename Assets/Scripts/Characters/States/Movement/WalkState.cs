using Assets.Characters;
using Assets.Scripts.Characters.States.StateMachines;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Characters.States.Movement
{
    [System.Serializable]
    public class WalkState : GroundedState<WalkStateData>
    {
        private Vector2 direction;

        public float currentSpeed;

        public WalkStateData walkStateData;
        public float accelleration, decelaration;
        public WalkState(CharacterStateMachine _stateMachine, WalkStateData _stateData) : base(_stateMachine, _stateData)
        {

            if (rb == null || character == null)
            {
                Debug.LogError("Walk action requires a Rigidbody2D and BaseCharacter component");
                return;
            }
            
            walkStateData = stateData;
            accelleration = walkStateData.accelerationData.baseAcceleration;
            decelaration = walkStateData.decelarationData.baseDecelaration;
            currentSpeed = 0;
            direction = Vector2.zero;
        }
        //Value passed by playercontrls to handle direction
        public void SetDirection(Vector2 _direction)
        {
            direction = new Vector2(_direction.x, 0);
            if (direction.x > 0)
                character.SetFacingRight(true);
            else if (direction.x < 0)
                character.SetFacingRight(false);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            //animator.Play("MagicalGirl_Walk");
            
            accelleration = walkStateData.accelerationData.baseAcceleration;
        }
        public override void OnExit()
        {
            base.OnExit();
           //direction = Vector2.zero;
           // rb.velocity = new Vector2(0, rb.velocity.y);
        }

        public override void Update()
        {
            base.Update();

            if (currentSpeed == 0 && character.moveDireciton.x == 0)
            {
               // Debug.Log("Returning from walk");
                //stateMachine.movementStateMachine.SetState(stateMachine.movementStateMachine.idleState);
                //return;
            }
            else
            {
                if (character.moveDireciton.x==0)
                {
                    accelleration = walkStateData.decelarationData.baseDecelaration;
                }
                float targetSpeed = character.moveDireciton.x * character.moveSpeed;
                if (character.moveDireciton != Vector2.zero)
                {
                    currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, accelleration * Time.deltaTime);
                }
                else
                {
                    currentSpeed = Mathf.MoveTowards(currentSpeed, 0, decelaration * Time.deltaTime);
                }
                currentSpeed = Math.Clamp(currentSpeed, walkStateData.velocityData.minVelocity, walkStateData.velocityData.maxVelocity);
                //Debug.Log(rb.velocity);
                rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
            }

        }
    }
}
