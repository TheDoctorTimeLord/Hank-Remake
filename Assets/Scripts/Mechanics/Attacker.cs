using System;
using System.Collections;
using Core;
using Gameplay.Level;
using JetBrains.Annotations;
using Mechanics.ScriptableObjects.AttackStrategy;
using UnityEngine;

namespace Mechanics
{
    [RequireComponent(typeof(HasDirection))]
    public class Attacker : MonoBehaviour
    {
        private static readonly int AttackParameter = Animator.StringToHash("attack");
        
        [SerializeField] private AttackStrategySO attackStrategy;
        [SerializeField] private float attackAnimationDuration;

        private LevelCapability levelCapability;
        private HasDirection hasDirection;
        [CanBeNull] private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            hasDirection = GetComponent<HasDirection>();
        }

        private void Start()
        {
            levelCapability = Simulation.GetCapability<LevelCapability>();
            attackStrategy.Initialize();
        }

        public void Attack()
        {
            StartCoroutine(AttackInt());
        }

        private IEnumerator AttackInt()
        {
            animator?.SetBool(AttackParameter, true);
            
            yield return new WaitForSeconds(attackAnimationDuration);
            
            animator?.SetBool(AttackParameter, false);

            var directionOffset = levelCapability.ConvertToOffset(hasDirection.Direction);
            yield return attackStrategy.Attack(gameObject, directionOffset);
        }
    }
}