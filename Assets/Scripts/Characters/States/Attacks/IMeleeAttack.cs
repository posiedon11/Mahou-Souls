//IMeleeAttack.cs
//All Melee attacks should implement this interface
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters.States.Attacks
{
    public interface IMeleeAttack
    {
        float damageRatio { get; set; }
        float rangeRatio { get; set; }
        void PerformMeleeAttack();
    }
}
