using Assets.Scripts.Characters.States.StateMachines;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.States
{
    public class EmptyState : BaseState<EmptyStateData>
    {
       public EmptyState(CharacterStateMachine _stateMachine, EmptyStateData _stateData) : base(_stateMachine, _stateData)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void Update()
        {
            base.Update();
        }
        public override bool CanExit()
        {
            return base.CanExit();
        }
    }
}
