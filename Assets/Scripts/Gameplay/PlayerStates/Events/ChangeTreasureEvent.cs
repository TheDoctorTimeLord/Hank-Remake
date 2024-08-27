using Core;

namespace Gameplay.PlayerStates.Events
{
    public class ChangeTreasureEvent : Simulation.Event<ChangeTreasureEvent>
    {
        public int ChangeTreasure;
        
        protected override void Execute()
        {
            var playerStatesCapability = Simulation.GetCapability<PlayerStatesCapability>();
            playerStatesCapability.Treasure += ChangeTreasure;
        }

        internal override void Cleanup()
        {
            ChangeTreasure = 0;
        }
    }
}