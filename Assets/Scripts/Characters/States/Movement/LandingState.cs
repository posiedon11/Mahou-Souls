using Assets.Scripts.Characters.States.StateMachines;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.States.Movement
{
    public class LandingState : GroundedState<LandingStateData>
    {
        public LandingState(CharacterStateMachine _stateMachine, LandingStateData _stateData) : base(_stateMachine, _stateData)
        {
            //jumpStateData = _stateData;
        }
    }
    
}
