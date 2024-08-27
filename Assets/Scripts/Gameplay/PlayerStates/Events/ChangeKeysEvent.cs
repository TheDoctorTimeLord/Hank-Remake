using Core;

namespace Gameplay.PlayerStates.Events
{
    public class ChangeKeysEvent : Simulation.Event<ChangeKeysEvent>
    {
        public int ChangeKeys;
        
        protected override void Execute()
        {
            var playerStatesCapability = Simulation.GetCapability<PlayerStatesCapability>();
            playerStatesCapability.Keys += ChangeKeys;
        }

        internal override void Cleanup()
        {
            ChangeKeys = 0;
        }
    }
}