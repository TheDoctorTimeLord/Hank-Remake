using Core;
using Gameplay.Level;
using Mechanics.ScriptableObjects;
using UnityEngine;

namespace Mechanics
{
    [RequireComponent(typeof(PlacedInGameField))]
    public class GameFieldCollider : MonoBehaviour
    {
        [SerializeField] private CollidingTypeSO colliderType;
        [SerializeField] private GameObjectCollider[] collidingRules;

        private void Start()
        {
            GetComponent<PlacedInGameField>().RunAfterInitialize(Initialize);
        }

        private void Initialize()
        {
            Simulation.GetCapability<LevelCapability>().RegisterCollider(gameObject, colliderType, collidingRules);
        }
    }
}