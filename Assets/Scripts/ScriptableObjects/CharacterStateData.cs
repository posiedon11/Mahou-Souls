using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CharacterStateData", menuName = "Character/StateData", order = 2)]
    public class CharacterStateData : ScriptableObject
    {
        public EmptyStateData emptyStateData;
        public IdleStateData idleStateData;
        public WalkStateData walkStateData;
        public JumpStateData jumpStateData;
        public FallStateData fallStateData;
        public LandingStateData landStateData;

        public OverHeadSwingData overHeadSwingData;
        public StaffStabData stabData;

    }
}
