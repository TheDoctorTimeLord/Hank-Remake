using System;
using System.Collections.Generic;
using Core;
using Core.Utils;
using Mechanics.ScriptableObjects;
using UnityEngine;

namespace Gameplay.Level
{
    public class LevelCapability : Capability
    {
        public Vector2Int Sides { get; }
        public Vector2 GetDistanceBetweenAdjustCell => _fieldGrid.cellSize + _fieldGrid.cellGap;
        public Vector2 SidesInWorldCoordinate => Sides * GetDistanceBetweenAdjustCell;

        private readonly EntityIdentifiersStorage _entityIdentifiersStorage;
        private readonly FieldInformationStorage _fieldInformationStorage;
        private readonly CollidingService _collidingService;
        
        private readonly Grid _fieldGrid;
        private readonly Dictionary<Direction, Vector2> _offsetsInField = new();

        public LevelCapability(Vector2Int sides, Grid fieldGrid)
        {
            Sides = sides;
            _fieldGrid = fieldGrid;
            
            var fullStep = GetDistanceBetweenAdjustCell;
            _offsetsInField[Direction.Up] = Vector2.up * fullStep.y;
            _offsetsInField[Direction.Down] = Vector2.down * fullStep.y;
            _offsetsInField[Direction.Left] = Vector2.left * fullStep.x;
            _offsetsInField[Direction.Right] = Vector2.right * fullStep.x;
            _offsetsInField[Direction.Empty] = Vector2.zero;
            
            _entityIdentifiersStorage = new EntityIdentifiersStorage();
            _fieldInformationStorage = new FieldInformationStorage(sides);
            _collidingService = new CollidingService();
        }

        public Vector2 ConvertToOffset(Direction direction)
        {
            return _offsetsInField[direction];
        }

        public static Vector2Int ConvertToGridOffset(Direction direction)
        {
            return direction switch
            { 
                    Direction.Up => Vector2Int.up, 
                    Direction.Left => Vector2Int.left,
                    Direction.Right => Vector2Int.right,
                    Direction.Down => Vector2Int.down,
                    _ => Vector2Int.zero
            };
        }
        
        public Vector2 GetWorldCoordinatesByCell(Vector2Int levelCellCoordinate)
        {
            return _fieldGrid.CellToWorld((Vector3Int)levelCellCoordinate) + _fieldGrid.cellSize / 2;
        }

        public Vector2Int GetCellCoordinatesByWorld(Vector2 worldCoordinate)
        {
            return (Vector2Int)_fieldGrid.WorldToCell(worldCoordinate);
        }

        public int RegisterEntity(GameObject registeredEntity, Vector2Int positionOnField)
        {
            var id = _entityIdentifiersStorage.RegisterObject(registeredEntity);
            _fieldInformationStorage.SetEntityPosition(id, positionOnField);
            return id;
        }

        public void RegisterCollider(GameObject colliderOwner, CollidingTypeSO colliderType, GameObjectCollider[] collidingRules)
        {
            _collidingService.RegisterCollider(_entityIdentifiersStorage.Resolve(colliderOwner), colliderType, collidingRules);
        }

        public void Remove(GameObject removing)
        {
            var removingId = _entityIdentifiersStorage.RemoveObject(removing);
            _fieldInformationStorage.RemoveEntity(removingId);
            _collidingService.RemoveCollider(removingId);
        }

        public void ReserveNextPositionToMove(GameObject movable, Direction direction)
        {
            var resolvedId = _entityIdentifiersStorage.Resolve(movable);
            var position = _fieldInformationStorage.GetEntityCurrentPosition(resolvedId) + ConvertToGridOffset(direction);
            _fieldInformationStorage.SetPlacementReservation(resolvedId, position);
        }

        public bool HasReservedPosition(GameObject movable)
        {
            var resolvedId = _entityIdentifiersStorage.Resolve(movable);
            return _fieldInformationStorage.HasReservedPosition(resolvedId);
        }

        public void MoveObjectToReservedPosition(GameObject moved)
        {
            var resolvedId = _entityIdentifiersStorage.Resolve(moved);
            var entityOnReservation = _fieldInformationStorage.MoveEntityToReservation(resolvedId);
            _collidingService.InteractionWith(_entityIdentifiersStorage, moved, entityOnReservation);
        }

        public bool CanEntityMoveTo(GameObject entity, Direction moveTo)
        {
            var resolvedId = _entityIdentifiersStorage.Resolve(entity);
            var position = _fieldInformationStorage.GetEntityCurrentPosition(resolvedId) + ConvertToGridOffset(moveTo);
            var entities = _fieldInformationStorage.GetEntitiesOnPosition(position);

            return _fieldInformationStorage.ContainsInFieldBounds(position) &&
                   _collidingService.CanMoveToPosition(_entityIdentifiersStorage, entity, entities);
        }

        public void Interact(GameObject interacted)
        {
            var resolvedId = _entityIdentifiersStorage.Resolve(interacted);
            if (!_fieldInformationStorage.IsPresentOnField(resolvedId)) return;

            var currentPosition = _fieldInformationStorage.GetEntityCurrentPosition(resolvedId);
            var entities = _fieldInformationStorage.GetEntitiesOnPosition(currentPosition);
            _collidingService.InteractionWith(_entityIdentifiersStorage, interacted, entities);
        }

        public bool ContainsInFieldBounds(Vector2Int position)
        {
            return _fieldInformationStorage.ContainsInFieldBounds(position);
        }

        public override void Update()
        {
        }
    }
}