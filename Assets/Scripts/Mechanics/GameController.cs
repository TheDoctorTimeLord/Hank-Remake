using System;
using Core;
using Gameplay.Events;
using Gameplay.Level;
using Gameplay.PlayerStates;
using Gameplay.PlayerStates.Events;
using Model;
using UnityEngine;

namespace Mechanics
{
    public class GameController : MonoBehaviour, Simulation.ICoroutineRunner
    {
        [SerializeField] private Vector2Int gameFieldSides;
        [SerializeField] private Grid fieldGrid;
        [SerializeField] private float secondsBetweenCapabilitiesUpdate;

        [SerializeField] private int playerHealth;
        [SerializeField] private int playerWeaponBullets;
        
        private void Awake()
        {
            Simulation.Initialize(this);
            
            Simulation.GetModel<MainGameModel>().CapabilitiesManager
                    .RegisterCapabilityIfAbsent(new PlayerStatesCapability())
                    .RegisterCapability(new LevelCapability(gameFieldSides, fieldGrid));

            Simulation.Schedule<CapabilitiesUpdateEvent>().SecondsBetweenUpdates = secondsBetweenCapabilitiesUpdate;
        }

        private void Start()
        {
            Simulation.Schedule<ChangeHealthEvent>().ChangeHealth = playerHealth;
            Simulation.Schedule<ChangeWeaponEvent>().ChangeWeapon = playerWeaponBullets;
        }

        private void Update()
        {
            Simulation.Tick();
        }
    }
}
