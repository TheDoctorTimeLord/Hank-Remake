using Core;

namespace Gameplay.PlayerStates.Events
{
    public class ChangeHealthEvent : Simulation.Event<ChangeHealthEvent>
    {
        public int ChangeHealth;
        
        protected override void Execute()
        {
            var playerStatesCapability = Simulation.GetCapability<PlayerStatesCapability>();
            playerStatesCapability.Health += ChangeHealth;
        }

        internal override void Cleanup()
        {
            ChangeHealth = 0;
        }
    }
}