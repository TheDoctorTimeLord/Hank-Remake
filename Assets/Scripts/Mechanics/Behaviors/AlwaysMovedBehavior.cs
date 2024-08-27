using System;
using Core;
using Gameplay.Damaging;
using Gameplay.Level;
using Gameplay.Level.Events;
using UnityEngine;

namespace Mechanics.Behaviors
{
    [RequireComponent(typeof(MovableObject), typeof(HasDirection))]
    public class AlwaysMovedBehavior : MonoBehaviour
    {
        private MovableObject _movableObject;
        private HasDirection _hasDirection;
        private LevelCapability _levelCapability;

        private void Awake()
        {
            _movableObject = GetComponent<MovableObject>();
            _hasDirection = GetComponent<HasDirection>();
        }

        private void Start()
        {
            _levelCapability = Simulation.GetCapability<LevelCapability>();
        }

        private void Update()
        {
            if (_movableObject.IsMoving) return;

            var movingDirection = _hasDirection.Direction;

            if (_levelCapability.CanEntityMoveTo(gameObject, movingDirection))
            {
                var ev = Simulation.Schedule<MoveObjectIntentEvent>();
                ev.Movable = gameObject;
                ev.MovingDirection = movingDirection;
            }
            else
            {
                var ev = Simulation.Schedule<KillEvent>();
                ev.Killed = gameObject;
            }
        }
    }
}