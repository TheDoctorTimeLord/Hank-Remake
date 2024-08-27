using System;
using Core;
using Gameplay.Level;
using UnityEngine;

namespace Mechanics
{
    [RequireComponent(typeof(PlacedInGameField))]
    public class MovableObject : MonoBehaviour
    {
        public Action<float> MovementProgress;
        public bool IsMoving { get; private set; }
        
        [Range(.125f, 1f)]
        [SerializeField] private float secondsForMove;

        private LevelCapability _levelCapability;
        private Vector3 _startMoving;
        private Vector3 _targetMovingOffset;
        private float _currentMovementProgress = 1f;

        private void Awake()
        {
            GetComponent<HasDirection>();
        }

        private void Start()
        {
            _levelCapability = Simulation.GetCapability<LevelCapability>();
        }

        private void Update()
        {
            if (_currentMovementProgress >= 1) ResetMovement();
            if (_targetMovingOffset == Vector3.zero) return;

            UpdateMove();
        }

        private void UpdateMove()
        {
            _currentMovementProgress += _targetMovingOffset.magnitude * Time.deltaTime / secondsForMove;
            transform.position = Vector3.Lerp(_startMoving, _startMoving + _targetMovingOffset, _currentMovementProgress);
            
            MovementProgress?.Invoke(Mathf.Clamp01(_currentMovementProgress));
        }
        
        private void ResetMovement()
        {
            _currentMovementProgress = 0f;
            _startMoving = transform.position;
            _targetMovingOffset = Vector3.zero;
            IsMoving = false;
            
            MovementProgress?.Invoke(0f);
        }
        
        public void SetTargetMoveOffset(Direction movingDirection)
        {
            if (IsMoving || _currentMovementProgress > 0) return;
            
            var offset = _levelCapability.ConvertToOffset(movingDirection);
            _targetMovingOffset = new Vector3(offset.x, offset.y, transform.position.z);
            IsMoving = true;
        }
    }
}