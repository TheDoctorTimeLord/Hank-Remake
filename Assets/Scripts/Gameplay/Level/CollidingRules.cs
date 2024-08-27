using System;
using System.Collections.Generic;
using System.Linq;
using Mechanics.ScriptableObjects;
using Mechanics.ScriptableObjects.InteractionEffects;
using Mechanics.ScriptableObjects.PassabilityChecker;
using UnityEngine;

namespace Gameplay.Level
{
    public class CollidingRules
    {
        public static readonly CollidingRules Empty = new(Array.Empty<GameObjectCollider>());

        private static readonly PassabilityCheckerSO DefaultPassabilityChecker =
                ScriptableObject.CreateInstance<DefaultPassabilityCheckerSO>();
        private static readonly InteractionEffectSO DefaultInteractionEffect =
                ScriptableObject.CreateInstance<DefaultInteractionEffectSO>();
        private static readonly ColliderRule DefaultRule = new(DefaultPassabilityChecker, DefaultInteractionEffect);
        
        private readonly Dictionary<ColliderInfo, ColliderRule> _rules;

        public CollidingRules(GameObjectCollider[] rules)
        {
            _rules = rules
                    .Select(collider => new Tuple<ColliderInfo, ColliderRule>(
                            new ColliderInfo(collider.PlacementType, collider.CollidingType),
                            new ColliderRule(
                                    collider.PassabilityChecker ?? DefaultPassabilityChecker, 
                                    collider.InteractionEffect ?? DefaultInteractionEffect
                            )
                    ))
                    .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
        }

        public ColliderRule GetRule(ColliderInfo colliderInfo)
        {
            return _rules.GetValueOrDefault(colliderInfo, DefaultRule);
        }
    }
    
    public readonly struct ColliderInfo
    {
        public PlacementType PlacementType { get; }
        public CollidingTypeSO ColliderType { get; }

        public ColliderInfo(PlacementType placementType, CollidingTypeSO colliderType)
        {
            PlacementType = placementType;
            ColliderType = colliderType;
        }

        public override bool Equals(object obj)
        {
            return obj is ColliderInfo colliderInfo && Equals(colliderInfo);
        }

        public bool Equals(ColliderInfo other)
        {
            return PlacementType == other.PlacementType && ColliderType == other.ColliderType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)PlacementType, ColliderType);
        }

        public override string ToString()
        {
            return $"{nameof(ColliderInfo)} {{{nameof(PlacementType)}={PlacementType}, {nameof(ColliderType)}={ColliderType}}}";
        }
    }

    public struct ColliderRule
    {
        public PassabilityCheckerSO PassabilityChecker { get; }
        public InteractionEffectSO InteractionEffect { get; }

        public ColliderRule(PassabilityCheckerSO passabilityChecker, InteractionEffectSO interactionEffect)
        {
            PassabilityChecker = passabilityChecker;
            InteractionEffect = interactionEffect;
        }
    }
}