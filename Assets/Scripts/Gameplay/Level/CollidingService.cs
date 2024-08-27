using System;
using System.Collections.Generic;
using Mechanics.ScriptableObjects;
using UnityEngine;

namespace Gameplay.Level
{
    public class CollidingService
    {
        private readonly Dictionary<int, CollidingTypeSO> _collidingTypes = new();
        private readonly Dictionary<int, CollidingRules> _rules = new(); 

        public void RegisterCollider(int entityId, CollidingTypeSO collidingType, GameObjectCollider[] rules)
        {
            if (_collidingTypes.TryGetValue(entityId, out var actualCollidingType))
            {
                throw new ArgumentException($"Colliding type for entity [{entityId}] was registered already. Set: " +
                                            $"[{collidingType}], Actual: [{actualCollidingType}]");
            }
            _collidingTypes[entityId] = collidingType;
            _rules[entityId] = new CollidingRules(rules);
        }

        public void RemoveCollider(int removingId)
        {
            _collidingTypes.Remove(removingId);
            _rules.Remove(removingId);
        }

        public bool CanMoveToPosition(IIdResolver resolver, GameObject moving, IEnumerable<GameObjectPlacement> placedEntities)
        {
            var movingRules = GetRules(resolver.Resolve(moving));
            
            foreach (var placement in placedEntities)
            {
                if (!_collidingTypes.TryGetValue(placement.LinkedObjectId, out var collidingType)) continue;

                var colliderInfo = new ColliderInfo(placement.Type, collidingType);
                var colliderRule = movingRules.GetRule(colliderInfo);
                if (!colliderRule.PassabilityChecker.CanMoveToThisPosition(moving, resolver.Resolve(placement.LinkedObjectId)))
                    return false;
            }

            return true;
        }

        public void InteractionWith(IIdResolver resolver, GameObject interacted, IEnumerable<GameObjectPlacement> with)
        {
            var interactedRules = GetRules(resolver.Resolve(interacted));
            
            foreach (var placement in with)
            {
                if (!_collidingTypes.TryGetValue(placement.LinkedObjectId, out var collidingType)) continue;

                var colliderInfo = new ColliderInfo(placement.Type, collidingType);
                var colliderRule = interactedRules.GetRule(colliderInfo);
                colliderRule.InteractionEffect.InteractionWith(interacted, resolver.Resolve(placement.LinkedObjectId));
            }
        }

        private CollidingRules GetRules(int rulesOwnerId)
        {
            return _rules.GetValueOrDefault(rulesOwnerId, CollidingRules.Empty);
        }
    }
}