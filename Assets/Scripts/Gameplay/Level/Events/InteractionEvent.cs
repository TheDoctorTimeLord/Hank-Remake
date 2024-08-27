using Core;
using UnityEngine;

namespace Gameplay.Level.Events
{
    public class InteractionEvent : Simulation.Event<InteractionEvent>
    {
        public GameObject Interacted;
        
        protected override void Execute()
        {
            var levelCapability = Simulation.GetCapability<LevelCapability>();
            levelCapability.Interact(Interacted);
        }

        internal override void Cleanup()
        {
            Interacted = null;
        }
    }
}