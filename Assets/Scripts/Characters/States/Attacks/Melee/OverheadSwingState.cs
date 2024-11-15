using Assets.Characters;
using Assets.Scripts.Characters.States.StateMachines;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                canInterrupt = false;
                attacker = character;
                hitboxes = FindHitboxes("Attacks/OverheadSwing/HurtBoxes");
                swingData = _stateData;

                damageRatio = swingData.meleeAttackData.damageRatio;
                rangeRatio = swingData.meleeAttackData.rangeRatio;

                attackDamage = swingData.damageData.baseDamage;
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
            Debug.Log($"Clearing hit enemies Before OnEnter {hitEnemies.Count()}, {knockbackEnemies.Count()}");

            base.OnEnter();
            ReduceResources();
            knockbackEnemies.Clear();
            hitEnemies.Clear();

            hitEnemies = new List<BaseCharacter>();
            knockbackEnemies = new List<BaseCharacter>();

            damageRatio = swingData.meleeAttackData.damageRatio;
            rangeRatio = swingData.meleeAttackData.rangeRatio;
            attackDamage = swingData.damageData.baseDamage;
            Debug.Log($"Clearing hit enemies After OnEnter {hitEnemies.Count()}, {knockbackEnemies.Count()}");

            //animator.SetTrigger("MagicalGirl_OverHead_Swing");
            //animator.Play("MagicalGirl_OverHead_Swing");
        }

        public override void OnExit()
        {
           Debug.Log($"Clearing hit enemies Before OnExit {hitEnemies.Count()}, {knockbackEnemies.Count()}");

            base.OnExit();
            hitEnemies.Clear();
            knockbackEnemies.Clear();


            stateMachine.SetDefaultState();
            Debug.Log($"Clearing hit enemies After OnExit {hitEnemies.Count()}, {knockbackEnemies.Count()}");
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
        //applies damage to the victim, possible to add something to attacker as well
        private void ApplyDamage(BaseCharacter victim)
        {
            float damageAmount =Mathf.Clamp( (attacker.attackDamage + attackDamage) * damageRatio, 0, float.MaxValue);
            Debug.Log($"Character Damage: {attacker.attackDamage}, ActionDamage{swingData.damageData.baseDamage}, DamageRatio: {damageRatio}, RangeRatio: {rangeRatio}");
            Debug.Log($"Applying damage ({damageAmount}) to {victim.name} from {actionName}");
            DamageInstance damageInstance = new DamageInstance(attacker, victim, damageAmount, DamageType.Physical, null);
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
                    Debug.Log("Hitbox triggered For faction");
                    if (hitboxName == "KnockBack")
                    {
                        if (knockbackEnemies.Contains(victim))
                            Debug.Log("Enemy already knockebacked");
                        else
                        {
                            Debug.Log("Knockback applied");
                            victim.ApplyKnockback(attacker.transform.position, swingData.baseSwingForce);
                            knockbackEnemies.Add(victim);
                        }
                    }
                    else if (hitboxName == "Swing")
                    {
                        if(hitEnemies.Contains(victim))
                        {
                            Debug.Log("Enemy already hit");
                        }
                        else
                        {
                            hitEnemies.Add(victim);
                            Debug.Log($"Hitenemies count after applied damage: {hitEnemies.Count()}");
                            ApplyDamage(victim);
                        }
                    }
                    else
                    {
                        Debug.Log("Not correct hitbox name");
                    }
                }
            }
        }
    }
}
