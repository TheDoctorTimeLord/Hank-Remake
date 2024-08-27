using System;
using Mechanics.ScriptableObjects;
using Mechanics.ScriptableObjects.InteractionEffects;
using Mechanics.ScriptableObjects.PassabilityChecker;
using UnityEngine;

namespace Gameplay.Level
{
    [Serializable]
    public struct GameObjectCollider
    {
        [SerializeField] private PlacementType placementType;
        [SerializeField] private CollidingTypeSO collidingType;
        [SerializeField] private PassabilityCheckerSO passabilityChecker;
        [SerializeField] private InteractionEffectSO interactionEffect;

        public PlacementType PlacementType => placementType;
        public CollidingTypeSO CollidingType => collidingType;
        public PassabilityCheckerSO PassabilityChecker => passabilityChecker;
        public InteractionEffectSO InteractionEffect => interactionEffect;
        
        public GameObjectCollider(PlacementType placementType, CollidingTypeSO collidingType, 
                PassabilityCheckerSO passabilityChecker, InteractionEffectSO interactionEffect)
        {
            this.placementType = placementType;
            this.collidingType = collidingType;
            this.passabilityChecker = passabilityChecker;
            this.interactionEffect = interactionEffect;
        }

        public override bool Equals(object obj)
        {
            return obj is GameObjectCollider collider && Equals(collider);
        }

        public bool Equals(GameObjectCollider other)
        {
            return PlacementType == other.PlacementType && CollidingType == other.CollidingType 
                && Equals(PassabilityChecker, other.PassabilityChecker) && Equals(InteractionEffect, other.InteractionEffect);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)PlacementType, CollidingType, PassabilityChecker, InteractionEffect);
        }
    }
}