//DamageInstance.cs
//Class that represents a damage instance
//An instance of damage dealt from one character to another
using Assets.Characters;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public enum DamageType
    {
        Physical,
        Magical,
        True, 
        Elemental
    }
    public enum ElementalType
    {
        Fire,
        Water,
        Earth,
        Air
    }
    public class DamageInstance
    {
        public BaseCharacter Attacker { get; private set; }
        public BaseCharacter Reciever { get; private set; }


        protected DamageType DamageType { get; private set; }
        protected ElementalType? ElementalType { get; private set; }
        public float damageAmount { get; private set; }

        public DamageInstance(BaseCharacter attacker, BaseCharacter reciever, float damageAmount, DamageType damageType, ElementalType? elementalType = null)
        {
            Attacker = attacker;
            Reciever = reciever;
            DamageType = damageType;
            if (damageType == DamageType.Elemental && ElementalType != null)
            {
                ElementalType = elementalType;
            }
            this.damageAmount = damageAmount;
        }

        public float CalculateDamageDealt()
        {
            return damageAmount;
        }

        public void printDamageInstance()
        {
            Debug.Log($"Damage Instance\n: {Attacker.name} dealt {damageAmount} {DamageType} damage to {Reciever.name}");
        }
    }
}
