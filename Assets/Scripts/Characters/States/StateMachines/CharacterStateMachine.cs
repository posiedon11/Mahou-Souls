using Assets.Characters;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.Characters.States.StateMachines
{
    public class CharacterStateMachine : MonoBehaviour
    {
        public Animator animator;
        public SpriteRenderer spriteRenderer;
        //public BaseState currentState;

        public Rigidbody2D rb;
        public BaseCharacter character;

        public List<StateMachine> stateMachines = new  List<StateMachine>();
        public virtual void Awake()
        {
            try
            {

                rb = GetComponent<Rigidbody2D>();
                character = GetComponent<BaseCharacter>();
                animator = GetComponent<Animator>();
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error initializing CharacterStateMachine: {e}");
            }
        }

        public virtual void SetDefaultState()
        {
        }

        public virtual void SetState(BaseState newState)
        {
        }
        public virtual void SetNextState(BaseState nextState)
        {
        }
        protected virtual void InitializeStates() { }
        public virtual void Update()
        {
           foreach(StateMachine state in stateMachines)
            {
               state.Update();
            }
        }
        public virtual void FixedUpdate()
        {
            foreach (StateMachine state in stateMachines)
            {
                state.FixedUpdate();
            }
        }
    }
}
