using Core;
using Gameplay.Events;
using Gameplay.Level;
using Gameplay.Level.Events;
using UnityEngine;

namespace Mechanics.Behaviors
{
    [RequireComponent(typeof(MovableObject))]
    public class PlayerBehavior : MonoBehaviour
    {
        private MovableObject _movableObject;
        private Attacker _attacker;

        private void Start()
        {
            _movableObject = GetComponent<MovableObject>();
            _attacker = GetComponent<Attacker>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _attacker.Attack();
            }
            
            if (_movableObject.IsMoving) return;
            
            Direction movingDirection;
            if (Input.GetKey(KeyCode.W)) movingDirection = Direction.Up;
            else if (Input.GetKey(KeyCode.A)) movingDirection = Direction.Left;
            else if (Input.GetKey(KeyCode.S)) movingDirection = Direction.Down;
            else if (Input.GetKey(KeyCode.D)) movingDirection = Direction.Right;
            else return;
            
            var ev = Simulation.Schedule<MoveObjectIntentEvent>();
            ev.Movable = gameObject;
            ev.MovingDirection = movingDirection;
        }
    }
}