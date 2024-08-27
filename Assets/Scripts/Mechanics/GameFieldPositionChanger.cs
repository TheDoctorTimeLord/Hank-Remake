using System;
using Core;
using Gameplay.Level;
using UnityEngine;

namespace Mechanics
{
    [RequireComponent(typeof(MovableObject))]
    public class GameFieldPositionChanger : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float moveObjectThreshold;

        private LevelCapability _levelCapability;
        private MovableObject _movableObject;
        private bool _wasThresholdAchieved;

        private void Awake()
        {
            _movableObject = GetComponent<MovableObject>();
        }

        private void Start()
        {
            _levelCapability = Simulation.GetCapability<LevelCapability>();
        }

        private void OnEnable()
        {
            _movableObject.MovementProgress += OnChangeMoving;
        }

        private void OnDisable()
        {
            _movableObject.MovementProgress -= OnChangeMoving;
        }

        private void OnChangeMoving(float value)
        {
            if (value < moveObjectThreshold)
            {
                _wasThresholdAchieved = false;
            }
            else if (!_wasThresholdAchieved)
            {
                _wasThresholdAchieved = true;
                _levelCapability.MoveObjectToReservedPosition(gameObject);
            }
        }
    }
}