using System;
using System.Collections;
using Core;
using Gameplay.Level;
using Gameplay.Level.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.Behaviors
{
    [RequireComponent(typeof(MovableObject))]
    public class DestroyerBehavior : MonoBehaviour
    {
        [SerializeField] private float waitTime;
        [SerializeField] private int stepForWaiting;
        
        private LevelCapability _levelCapability;
        private MovableObject _movableObject;
        private Vector2Int _maxSteps;

        private bool _isWaited;
        private int _stepCount;
        private Vector2Int _target;

        private void Start()
        {
            _levelCapability = Simulation.GetCapability<LevelCapability>();
            _movableObject = GetComponent<MovableObject>();
            _maxSteps = new Vector2Int(stepForWaiting, stepForWaiting);
        }

        private void Update()
        {
            if (_isWaited || _movableObject.IsMoving) return;

            var selfCellCoordinates = _levelCapability.GetCellCoordinatesByWorld(transform.position);
            if (_target == Vector2Int.zero)
                _target = GetNewRandomTarget(selfCellCoordinates);

            if (_stepCount < stepForWaiting && selfCellCoordinates != _target)
                MoveToPosition(GetNextDirection(selfCellCoordinates));
            else
                StartCoroutine(Wait());
        }

        private Vector2Int GetNewRandomTarget(Vector2Int selfCellCoordinates)
        {
            var levelBounds = _levelCapability.Sides;
            
            var minTargetPositions = selfCellCoordinates - _maxSteps;
            minTargetPositions.Clamp(Vector2Int.zero, levelBounds);
            
            var maxTargetPositions = selfCellCoordinates + _maxSteps;
            maxTargetPositions.Clamp(Vector2Int.zero, levelBounds);

            return new Vector2Int(
                    Random.Range(minTargetPositions.x, maxTargetPositions.x),
                    Random.Range(minTargetPositions.y, maxTargetPositions.y)
            );
        }

        private void MoveToPosition(Direction moveTo)
        {
            _stepCount++;
            
            var ev = Simulation.Schedule<MoveObjectIntentEvent>();
            ev.Movable = gameObject;
            ev.MovingDirection = moveTo;
        }

        private IEnumerator Wait()
        {
            _isWaited = true;
            _stepCount = 0;
            _target = Vector2Int.zero;
            yield return new WaitForSeconds(waitTime);
            _isWaited = false;
        }

        private Direction GetNextDirection(Vector2Int selfCellCoordinates) //TODO может застрять на непроходимых тайлах
        {
            var offsetToTarget = _target - selfCellCoordinates;
            return CheckMaxDistance(
                    selfCellCoordinates, 
                    _target, 
                    offsetToTarget.x >= 0 ? Direction.Right : Direction.Left,
                    offsetToTarget.y >= 0 ? Direction.Up : Direction.Down
            );
        }

        private static Direction CheckMaxDistance(Vector2Int start, Vector2Int finish, Direction byX, Direction byY)
        {
            return Math.Abs(start.x - finish.x) >= Math.Abs(start.y - finish.y) ? byX : byY;
        }
    }
}