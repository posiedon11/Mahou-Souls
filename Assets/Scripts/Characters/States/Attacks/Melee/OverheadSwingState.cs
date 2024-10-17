using Assets.Characters;
using Assets.Scripts.Characters.States.StateMachines;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace Assets.Scripts.Characters.States.Attacks.Melee
{
    public class OverheadSwingState : BaseAttackState<OverHeadSwingData>, IMeleeAttack
    {
        [SerializeField]
        private GameObject OverHeadSwing;
        //[SerializeField] 
        private IEnumerable<Hitbox> hitboxes;


        public float damageRatio { get; set; }
        public float rangeRatio { get; set; }

        private List<BaseCharacter> hitEnemies = new List<BaseCharacter>();
        private List<BaseCharacter> knockbackEnemies = new List<BaseCharacter>();

        private BaseCharacter attacker;
        private OverHeadSwingData swingData;
        public OverheadSwingState(CharacterStateMachine _stateMachine, OverHeadSwingData _stateData) : base(_stateMachine, _stateData)
        {
            try
            {                
                attacker = character;
                hitboxes = FindHitboxes("Attacks/OverheadSwing/HurtBoxes");
                swingData = _stateData;

                damageRatio = swingData.meleeAttackData.damageRatio;
                rangeRatio = swingData.meleeAttackData.rangeRatio;

                Debug.Log(hitboxes.Count());
                foreach (Hitbox hitbox in hitboxes)
                {
                    hitbox.Initialize(this, "Characters");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error initializing OverheadSwingState: {e.Message}");
            }
        }

        //Sets the attacker of the swing and gets the hitboxes
        public override void OnEnter()
        {
            base.OnEnter();
            ReduceResources();
            damageRatio = swingData.meleeAttackData.damageRatio;
            rangeRatio = swingData.meleeAttackData.rangeRatio;
            attackDamage = swingData.damageData.baseDamage;
            //animator.SetTrigger("MagicalGirl_OverHead_Swing");
            //animator.Play("MagicalGirl_OverHead_Swing");
        }

        public override void OnExit()
        {
            base.OnExit();
            hitEnemies.Clear();
            knockbackEnemies.Clear();
            stateMachine.SetDefaultState();
            //stateMachine.actionStateMachine.SetDefaultState();
        }
        //inherited function from IMeleeAttack
        public void PerformMeleeAttack()
        {
            //OverHeadSwing.GetComponent<SpriteRenderer>().enabled = true;


        }

        //inherited function from BaseAttack
        public override void PerformAttack()
        {
            PerformMeleeAttack();
        }
        //Deactivates the hitbox after a time
        //not used anymore
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log($"Sword hit {collision.gameObject.name}");
            BaseCharacter character = collision.GetComponent<BaseCharacter>();
            if (character != null && !hitEnemies.Contains(character))
            {
                if (character.faction == CharacterFaction.Enemy)
                {
                    hitEnemies.Add(character);
                    ApplyDamage(character);
                }
            }
            //Debug.Log("Sword hit something");
        }

        //applies damage to the victim, possible to add something to attacker as well
        private void ApplyDamage(BaseCharacter victim)
        {
            Debug.Log($"Applying damage to {victim.name} from {actionName}");
            DamageInstance damageInstance = new DamageInstance(attacker, victim, (attacker.attackDamage + attackDamage) * damageRatio, DamageType.Physical, null);
            damageInstance.printDamageInstance();
            victim.TakeDamage(damageInstance);
        }

        public override void OnHitboxTrigger(Collider2D collision, string hitboxName)
        {
            base.OnHitboxTrigger(collision, hitboxName);
            BaseCharacter victim = collision.GetComponent<BaseCharacter>();
            if (victim != null)
            {
                if (attacker.faction != victim.faction)
                {
                    Debug.Log("Hitbox triggered");
                    if (hitboxName == "KnockBack" && !knockbackEnemies.Contains(victim))
                    {
                        Debug.Log("Knockback applied");
                        victim.ApplyKnockback(attacker.transform.position, swingData.baseSwingForce);
                        knockbackEnemies.Add(victim);
                    }
                    else if (hitboxName == "Swing" && !hitEnemies.Contains(victim))
                    {
                        hitEnemies.Add(victim);
                        ApplyDamage(victim);
                    }
                }
            }
        }
    }
}
