using Core;
using Mechanics;
using UnityEngine;

namespace Gameplay.Level.Events
{
    public class MoveObjectIntentEvent : Simulation.Event<MoveObjectIntentEvent>
    {
        public bool WasMovingSuccess { get; private set; }
        
        public GameObject Movable;
        public Direction MovingDirection;
        
        protected override void Execute()
        {
            var levelCapability = Simulation.GetCapability<LevelCapability>();
            if (MovingDirection == Direction.Empty || !Movable) return;

            var hasDirection = Movable.GetComponent<HasDirection>();
            if (hasDirection) hasDirection.Direction = MovingDirection;

            var movableObject = Movable.GetComponent<MovableObject>();
            if (!movableObject || movableObject.IsMoving || !levelCapability.CanEntityMoveTo(Movable, MovingDirection)) 
                return;
            
            levelCapability.ReserveNextPositionToMove(Movable, MovingDirection);
            movableObject.SetTargetMoveOffset(MovingDirection);
            WasMovingSuccess = true;
        }

        internal override void Cleanup()
        {
            Movable = null;
            MovingDirection = Direction.Empty;
            WasMovingSuccess = false;
        }
    }
}