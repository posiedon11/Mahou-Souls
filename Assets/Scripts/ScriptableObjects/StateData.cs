using Assets.Scripts.Characters.States.Attacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public class StateData 
    {
        BaseStateData stateData;
    }
    #region GenericData
    [System.Serializable]
    public class AccelerationData
    {
        public float baseAcceleration = 1, minAcceleration = 1, maxAcceleration = 1f;
    }
    [System.Serializable]
    public class DecelarationData
    {
        public float baseDecelaration = 1, minDecelaration = 1, maxDecelaration = 1f;
    }
    [System.Serializable]
    public class VelocityData
    {
        public float minVelocity = -5, maxVelocity = -5;
    }



    [System.Serializable]
    public class DamageData
    {
        public float baseDamage = 1, minDamage = 1, maxDamage = 1;
    }
    [System.Serializable]
    public class RangeData
    {
        public float baseRange = 1, minRange = 1, maxRange = 1;
    }

    [System.Serializable]
    public class IMeleeAttackData
    {
        public float damageRatio = 1, rangeRatio = 1;
    }
    #endregion


    [Serializable]
    public class  ResourceData
    {
        public float baseStaminaCost = 10, baseMagicCost = 0, baseHealthCost = 0;
    }
    

    [System.Serializable]
    public class BaseStateData
    {
        [Header("Base State Data")]
        public string actionName;
        public float baseActionDuration = 1;
        public ResourceData resourceData;
        public AnimationClip? onEnterAnimation;
        public AnimationClip? onExitAnimation;
    }
    #region MovementData

    [System.Serializable]
    public class GroundedStateData : BaseStateData
    {
        [Header("Grounded State Data")]
        public bool hangTimeEnabled;
        public float hangTime = 0.2f;
       // public BaseStateData baseStateData;
    }
    [System.Serializable]
    public class WalkStateData : GroundedStateData
    {
        //public GroundedStateData groundedStateData;
        //In case I want to have changing acceleration and deceleration

        public AccelerationData accelerationData;
        public DecelarationData decelarationData;
        public VelocityData velocityData;

        
    }
    [System.Serializable]

    public class JumpStateData : GroundedStateData
    {
        public float baseJumpForce = 1, minJumpForce = 1, maxJumpForce = 1;
    }

    [System.Serializable]
    public class FallStateData : BaseStateData
    {
        public AccelerationData accelerationData;
        public DecelarationData decelarationData;
        public VelocityData velocityData;
    }

    [Serializable]
    public class  IdleStateData : GroundedStateData
    {
        
    }

    [Serializable]
    public class LandingStateData : GroundedStateData
    {
        public BaseStateData baseStateData;
    }
    #endregion

    #region AttackData

    [System.Serializable]
    public class BaseAttackData : BaseStateData
    {
        [Header("Base Attack Data")]
        public DamageData damageData;

        public RangeData rangeData;
    }


    [System.Serializable]
    public class OverHeadSwingData : BaseAttackData
    {
       // public BaseAttackData baseAttackData;
        public IMeleeAttackData meleeAttackData;
        public float baseSwingForce = 1, minSwingForce = 1, maxSwingForce = 1;
    }
    [System.Serializable]
    public class  StaffStabData : BaseAttackData
    {
        //public BaseAttackData baseAttackData;
        public IMeleeAttackData meleeAttackData;
    }

    #endregion
}
