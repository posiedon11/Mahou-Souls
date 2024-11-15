//BaseCharacter.cs
//Base class for all characters
using Assets.Characters.Player;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Characters
{
    public enum CharacterFaction
    {
        Player,
        Enemy,
        Neutral
    }

    [System.Serializable]
    public class GroundCollision
    {
        public Vector2 feetBoxSize;
        public LayerMask groundLayers = 1;
        public float castDistance = .5f;
    }
    public abstract class BaseCharacter : MonoBehaviour
    {
        //the base values
        public float baseMaxHealth =100, baseMaxStamina = 100, baseMaxMana = 100, baseDamage = 1, baseMoveSpeed = 1, baseSprintSpeedMultiplier = 1.5f, baseJumpPower = 2f;
        // the current max values
        public float maxHealth = 100, maxStamina = 100, maxMana = 100, damage, moveSpeed, sprintSpeedMultiplier = 1.5f, jumpPower = 2f;
        //resource regen rates
        public float staminaRegenRate = 1, manaRegenRate = 1, healthRegenRate = 1;

        public bool grounded;
        public GroundCollision groundCollision;
        
        public CharacterFaction faction;

        //the current values(affect by other stats)
        public float currentHealth, currentStamina, currentMana, currentAttack, currentMoveSpeed, currentJumpPower;

        public bool facingRight = true;
        public Vector2 moveDireciton;


        public Rigidbody2D rb;
        public Animator animator;
        // Start is called before the first frame update



        public event System.Action<BaseCharacter> OnCharacterDeath;
        public bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, groundCollision.feetBoxSize, 0, Vector2.down, groundCollision.castDistance, groundCollision.groundLayers);
            if (hit)
            {
                //Debug.Log("Grounded");
                return true;
            }
            //Debug.Log("Not Grounded");
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position - transform.up * groundCollision.castDistance, groundCollision.feetBoxSize);
        }
        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            InitializeStats();

        }
        public void SetFacingRight(bool _facingRight)
        {
            if (facingRight != _facingRight)
            {
                animator.SetBool("FacingRight", _facingRight);
                base.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            this.facingRight = _facingRight;
        }

        protected virtual void Awake()
        {
        }
        public virtual void FixedUpdate()
        {
            grounded = IsGrounded();
            RegenerateStats();

        }

        public virtual void Update()
        {
        }

        public virtual void RegenerateStats()
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += healthRegenRate*Time.fixedDeltaTime;
            }
            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.fixedDeltaTime;
            }
            if (currentMana < maxMana)
            {
                currentMana += manaRegenRate * Time.fixedDeltaTime;
            }
        }

        protected virtual void RecalculateStats()
        {

        }
        public void TakeDamage(DamageInstance damage)
        {
            float damageDealt = damage.CalculateDamageDealt();
            currentHealth -= damageDealt;
            Debug.Log($"{rb.name} has taken {damage.CalculateDamageDealt()} damage from {damage.Attacker.name}");
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void ApplyKnockback(Vector2 direction, float force)
        {
            rb.AddForce(direction * force, ForceMode2D.Impulse);
        }
        public void Heal(float amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        protected virtual void Die()
        {
            //Debug.Log($"{rb.name} has died");
            OnCharacterDeath?.Invoke(this);
        }

        private void InitializeStats()
        {
            currentHealth = maxHealth;
            currentStamina = maxStamina;
            currentMana = maxMana;
            currentAttack = baseDamage;
            currentMoveSpeed = baseMoveSpeed;
            //currentJumpPower = baseJumpPower;
        }

        public float curHealth { get => currentHealth; }

        public float healthPercent { get => currentHealth / maxHealth; }
        public float staminaPercent { get => currentStamina / maxStamina; }
        public float manaPercent { get => currentMana / maxMana; }
        public float attackDamage { get => currentAttack; }
    }
}
