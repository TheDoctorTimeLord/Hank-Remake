using System.Collections;
using Core;
using Gameplay.Level.Events;
using Mechanics.ScriptableObjects.AttackChecking;
using UnityEngine;

namespace Mechanics.ScriptableObjects.AttackStrategy
{
    [CreateAssetMenu(menuName = "Attack")]
    public class AttackStrategySO : ScriptableObject
    {
        [SerializeField] private GameObject attackerEntityPrefab;
        [SerializeField] private AttackCheckerSO attackChecker;
        [SerializeField] private int instanceAttackerCount;
        [SerializeField] private float timeBetweenSpawnInstance;

        public void Initialize()
        {
            attackChecker.Initialize();
        }
        
        public IEnumerator Attack(GameObject attacker, Vector2 attackOffset)
        {
            if (!attackChecker.CanAttack()) yield break;
            
            var spawnPosition = attacker.transform.position + (Vector3)attackOffset;
            var attackerEntityDirection = attacker.GetComponent<HasDirection>().Direction;

            for (var i = 0; i < instanceAttackerCount; i++)
            {
                var attackerObject = Instantiate(attackerEntityPrefab);
                attackerObject.transform.position = spawnPosition;
                attackerObject.GetComponent<HasDirection>().Direction = attackerEntityDirection;

                var ev = Simulation.Schedule<InteractionEvent>();
                ev.Interacted = attackerObject;

                spawnPosition += (Vector3)attackOffset;
                yield return new WaitForSeconds(timeBetweenSpawnInstance);
            }
        }
    }
}