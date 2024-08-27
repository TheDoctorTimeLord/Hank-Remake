using Core;
using Model;
using UnityEngine;

namespace Gameplay.Events
{
    public class CapabilitiesUpdateEvent : Simulation.Event<CapabilitiesUpdateEvent>
    {
        private readonly CapabilitiesManager _manager = Simulation.GetModel<MainGameModel>().CapabilitiesManager;
        public float SecondsBetweenUpdates;
        
        protected override void Execute()
        {
            _manager.UpdateAllCapabilities();
            Simulation.Reschedule(this, Mathf.Clamp(SecondsBetweenUpdates, Simulation.MinimalDelay, float.PositiveInfinity));
        }
    }
}