using System;
using System.Collections.Generic;
using Core;
using Core.Attributes;
using Gameplay.Damaging;
using Gameplay.Level;
using UnityEngine;

namespace Mechanics
{
    public class PlacedInGameField : MonoBehaviour
    {
        private Action _initializeCallback;
        
        [SerializeField] public bool useAutomaticStartPosition;
        [ConditionalProperty("useAutomaticStartPosition", showIf = CheckingType.IfFalse)]
        [SerializeField] public Vector2Int startPosition;
        
        private bool _isInitialized;

        public LevelCapability LevelCapability { get; private set; }
        
        public int Id { get; private set; }
        
        private void Start()
        {
            Initialize();
            _initializeCallback?.Invoke();
            _isInitialized = true;
        }

        private void Initialize()
        {
            LevelCapability = Simulation.GetCapability<LevelCapability>();
            
            if (useAutomaticStartPosition)
            {
                startPosition = LevelCapability.GetCellCoordinatesByWorld(transform.position);
            }
            
            Id = LevelCapability.RegisterEntity(gameObject, startPosition);
            if (!LevelCapability.ContainsInFieldBounds(startPosition))
            {
                Simulation.Schedule<KillEvent>().Killed = gameObject;
            }
            
            var worldEntityCoordinates = LevelCapability.GetWorldCoordinatesByCell(startPosition);
            transform.position = new Vector3(worldEntityCoordinates.x, worldEntityCoordinates.y, transform.position.z);
        }

        public void RunAfterInitialize(Action callback)
        {
            if (_isInitialized) callback.Invoke();
            else _initializeCallback += callback;
        }

        private void OnDestroy()
        {
            LevelCapability.Remove(gameObject);
        }
    }
}