using Core;

namespace Gameplay.PlayerStates.Events
{
    public class ChangeBottleEvent : Simulation.Event<ChangeBottleEvent>
    {
        public int ChangeBottle;
        
        protected override void Execute()
        {
            var playerStatesCapability = Simulation.GetCapability<PlayerStatesCapability>();
            playerStatesCapability.Bottle += ChangeBottle;
        }

        internal override void Cleanup()
        {
            ChangeBottle = 0;
        }
    }
}