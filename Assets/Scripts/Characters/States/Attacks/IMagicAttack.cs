//IMagicAttack.cs
//All Magic attacks should implement this interface
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.Scripts.Characters.States.Attacks
{
    public interface IMagicAttack
    {
        void PrepareSpell();
        void CastSpell();
    }
}
