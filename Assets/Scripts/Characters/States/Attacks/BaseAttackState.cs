//BaseAttack.cs
//Base class for all attacks
using Assets.Characters;
using Assets.Scripts.Characters.States.StateMachines;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters.States.Attacks
{
    public abstract class BaseAttackState<TAttackData> : BaseState<TAttackData> where TAttackData : BaseAttackData
    {

        [SerializeField]
        public float attackDamage = 1, attackRange = 1, attackSpeed = 1;

        protected BaseAttackState(CharacterStateMachine _stateMachine, TAttackData _stateData) : base(_stateMachine, _stateData)
        {
        }

        public override void PerformAction()
        {
            base.PerformAction();
            PerformAttack();
        }

        public abstract void PerformAttack();

        public override bool CanAffordResources()
        {
            return base.CanAffordResources();
        }

    }
}
