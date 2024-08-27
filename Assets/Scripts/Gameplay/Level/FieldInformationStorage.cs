using System;
using System.Collections.Generic;
using System.Linq;
using Core.Utils;
using UnityEngine;

namespace Gameplay.Level
{
    public class FieldInformationStorage
    {
        private readonly Vector2Int NotPresentPosition = new(-1, -1);
        
        public Vector2Int Sides { get; }

        private readonly Dictionary<Vector2Int, List<GameObjectPlacement>> _placementByPosition = new();
        private readonly Dictionary<int, Vector2Int> _entityPosition = new();
        private readonly Dictionary<int, Vector2Int> _entityReservationPosition = new();

        public FieldInformationStorage(Vector2Int sides)
        {
            Sides = sides;
        }

        public void RemoveEntity(int removingId)
        {
            if (_entityPosition.Remove(removingId, out var entityPosition))
            {
                RemovePlacement(removingId, PlacementType.GameObjectInstance, entityPosition);
            }

            if (_entityReservationPosition.Remove(removingId, out var reservationPosition))
            {
                RemovePlacement(removingId, PlacementType.PlaceReservation, reservationPosition);
            }
        }

        public bool ContainsInFieldBounds(Vector2Int checkedPosition)
        {
            return MathUtils.ContainsInBounds(checkedPosition, Vector2.zero, Sides);
        }

        public IEnumerable<GameObjectPlacement> GetEntitiesOnPosition(Vector2Int position)
        {
            return _placementByPosition.TryGetValue(position, out var entities) 
                    ? entities 
                    : Enumerable.Empty<GameObjectPlacement>();
        }

        public bool IsPresentOnField(int entityId)
        {
            return _entityPosition.ContainsKey(entityId);
        }

        public Vector2Int GetEntityCurrentPosition(int entityId)
        {
            if (!_entityPosition.TryGetValue(entityId, out var position))
            {
                throw new ArgumentException($"Entity [{entityId}] was not registered");
            }
            return position;
        }

        public void SetEntityPosition(int entityId, Vector2Int position)
        {
            if (!ContainsInFieldBounds(position)) return;
            
            SetPlacement(entityId, PlacementType.GameObjectInstance, position);
            _entityPosition[entityId] = position;
            _entityReservationPosition.Remove(entityId);
        }

        public void SetPlacementReservation(int reservationOwnerId, Vector2Int reservationPosition)
        {
            var oldReservedPosition = _entityReservationPosition.GetValueOrDefault(reservationOwnerId, NotPresentPosition);
            if (oldReservedPosition != NotPresentPosition)
            {
                RemovePlacement(reservationOwnerId, PlacementType.PlaceReservation, oldReservedPosition);
            }

            _entityReservationPosition[reservationOwnerId] = reservationPosition;
            SetPlacement(reservationOwnerId, PlacementType.PlaceReservation, reservationPosition);
        }

        public IEnumerable<GameObjectPlacement> MoveEntityToReservation(int entityId)
        {
            var oldPosition = _entityPosition.GetValueOrDefault(entityId, NotPresentPosition);
            if (oldPosition == NotPresentPosition)
            {
                throw new ArgumentException($"Entity [{entityId}] is not found but tried to move");
            }

            var reservedPosition = _entityReservationPosition.GetValueOrDefault(entityId, NotPresentPosition);
            if (reservedPosition == NotPresentPosition)
            {
                throw new ArgumentException($"Entity [{entityId}] has no reserved position but tried to move");
            }
            
            RemovePlacement(entityId, PlacementType.PlaceReservation, reservedPosition);
            RemovePlacement(entityId, PlacementType.GameObjectInstance, oldPosition);

            var entitiesOnPosition = new List<GameObjectPlacement>(GetEntitiesOnPosition(reservedPosition));

            SetEntityPosition(entityId, reservedPosition);

            return entitiesOnPosition;
        }

        private void SetPlacement(int entityId, PlacementType placementType, Vector2Int position)
        {
            if (!_placementByPosition.ContainsKey(position))
            {
                _placementByPosition[position] = new List<GameObjectPlacement>();
            }
            _placementByPosition[position].Add(new GameObjectPlacement(placementType, entityId));
        }

        private void RemovePlacement(int entity, PlacementType placementType, Vector2Int position)
        {
            _placementByPosition[position].Remove(new GameObjectPlacement(placementType, entity));
        }

        public bool HasReservedPosition(int resolvedId)
        {
            return _entityReservationPosition.ContainsKey(resolvedId);
        }
    }
}