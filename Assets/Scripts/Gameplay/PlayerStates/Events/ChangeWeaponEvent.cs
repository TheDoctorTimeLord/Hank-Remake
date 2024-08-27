using Core;

namespace Gameplay.PlayerStates.Events
{
    public class ChangeWeaponEvent : Simulation.Event<ChangeWeaponEvent>
    {
        public int ChangeWeapon;
        
        protected override void Execute()
        {
            var playerStatesCapability = Simulation.GetCapability<PlayerStatesCapability>();
            playerStatesCapability.Weapon += ChangeWeapon;
        }

        internal override void Cleanup()
        {
            ChangeWeapon = 0;
        }
    }
}