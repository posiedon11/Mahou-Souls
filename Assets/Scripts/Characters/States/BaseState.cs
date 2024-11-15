//BaseAction.cs
//Base class for all actions
using Assets.Characters;
using Assets.Scripts.Characters.States.StateMachines;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.Characters.States
{
    public abstract class BaseState
    {
        public string actionName;

        protected Animator animator;
        protected BaseCharacter character;
        protected Rigidbody2D rb;
        protected CharacterStateMachine stateMachine;
        protected float staminaCost;
        protected float magicCost;
        protected float healthCost;
       
        protected string baseAnimationName = "";


        public bool canInterrupt = true, inProgress = true;
        protected float elapsedTime = 0, duration = 1;

        public virtual void OnEnter()
        {
            if (DebugSettings.stateMachineBehavior)
                Debug.Log($"Entering {actionName}");
            if (!CanAffordResources())
            {
                if (DebugSettings.stateMachineBehavior)

                    Debug.Log($"Cannot afford {actionName}");
                return;
            }
            ReduceResources();
            RecalculateStats();
            elapsedTime = 0f;
            inProgress = true;
        }
        public virtual void OnExit()
        {
            if (DebugSettings.stateMachineBehavior)
                Debug.Log($"Exiting {actionName}");
        }
        public virtual void FixedUpdate()
        {
            if (DebugSettings.stateMachineBehavior)
                Debug.Log($"Fixed updating {actionName}");
            ElapseTime();
        }
        public virtual void Update()
        {
            if (DebugSettings.stateMachineBehavior)
                Debug.Log($"Updating {actionName}");
        }

        public virtual void RecalculateStats()
        {

        }
        public virtual bool CanExit()
        {
            bool canExit = true;
            string outputMessage = "";

            if (!inProgress)
            {
                outputMessage += $"{actionName} is not in progress\n";
            }
            else if (canInterrupt)
            {
                outputMessage += $"{actionName} is in progress and can be interrupted\n";
            }
            else if (!canInterrupt)
            {
                outputMessage += $"{actionName} is in progress and cannot be interrupted\n";
                canExit = false;
            }

            if(DebugSettings.stateMachineTransations)
                Debug.Log(outputMessage);
            return canExit;
        }

        public virtual bool CanPreform() { return true; }
        public virtual bool CanAffordResources()
        {
            bool canPerform = true;
            string debugMessage = "";
            if (character.currentStamina < staminaCost)
            {
                debugMessage +=$"Not enough stamina to perform {actionName}\n";
                canPerform = false;
            }
            if (character.currentMana < magicCost)
            {
                debugMessage += $"Not enough mana to perform {actionName}\n";
                canPerform = false;
            }
            if (character.currentHealth < healthCost)
            {
                debugMessage += $"Not enough health to perform {actionName}\n";
                canPerform = false;
            }
            
            if (canPerform) { debugMessage += $"{actionName} can be performed\n"; }
                
            
            if (DebugSettings.stateMachineActions)
                Debug.Log(debugMessage);

            return canPerform;
        }

        public virtual void ReduceResources()
        {
            character.currentStamina = Math.Clamp(character.currentStamina-staminaCost, 0, character.maxStamina);
            character.currentMana = Math.Clamp(character.currentMana - magicCost,0,character.maxMana);
            character.currentHealth = Math.Clamp(character.currentHealth - healthCost,0,character.maxHealth);
        }
        private void ElapseTime()
        {
            elapsedTime += Time.fixedDeltaTime;
            if (elapsedTime >= duration || duration <=0)
            {
               // Debug.Log($"{actionName} duration reached");
                inProgress = false;
                //OnExit();
            }
        }
        public virtual void OnHitboxTrigger(Collider2D collision, string hitboxName)
        {
            Debug.Log($"{actionName} hitbox __{hitboxName}__ hit:   {collision.name}");
        }
        public virtual IEnumerable<Hitbox>? FindHitboxes(string objectPath)
        {

            IEnumerable<Hitbox>? hitboxes = character.transform.Find(objectPath).GetComponentsInChildren<Hitbox>();
            return hitboxes;
        }
    }
    public abstract class BaseState<TStateData> : BaseState where TStateData : BaseStateData
    {

        protected TStateData stateData;
        private BaseStateData baseStateData;



        #region old
        public virtual void PerformAction()
        {
            if (DebugSettings.stateMachineTransations)
            Debug.Log($"Performing {actionName}");
        }

        public virtual void StartAction()
        {
            if (DebugSettings.stateMachineTransations)

                Debug.Log($"Starting {actionName}");
        }
        public virtual void StopAction()
        {
            if (DebugSettings.stateMachineTransations)

                Debug.Log($"Stopping {actionName}");
        }
        #endregion


        public BaseState(CharacterStateMachine _stateMachine, TStateData _stateData)
        {

            stateMachine = _stateMachine;
            character = stateMachine.character;
            rb = stateMachine.rb;
            stateData = _stateData;
            duration = stateData.baseActionDuration;
            animator = stateMachine.animator;
            SetStateVariables();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (stateData.onEnterAnimation != null)
                animator.Play(stateData.onEnterAnimation.name);

        }
        public override void Update()
        {
            base.Update();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            //elapseTime()
                //negative duration means the action will not exit until the player does\

            
        }

        

        public override void OnExit()
        {
            base.OnExit();
            if (stateData.onExitAnimation != null)
                animator.Play(stateData.onExitAnimation.name);
        }
        
        private void SetResourceCosts()
        {
            base.staminaCost = stateData.resourceData.baseStaminaCost;
            magicCost = stateData.resourceData.baseMagicCost;
            healthCost = stateData.resourceData.baseHealthCost;
        }
        public void UpdateResourceCosts()
        {
            SetResourceCosts();
        }


        private void SetStateVariables()
        {
            SetResourceCosts();
            actionName = stateData.actionName;
        }

    }
}
