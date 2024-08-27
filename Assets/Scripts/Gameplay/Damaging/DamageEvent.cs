using Core;
using Mechanics;
using UnityEngine;

namespace Gameplay.Damaging
{
    public class DamageEvent : Simulation.Event<DamageEvent>
    {
        public GameObject Damaging;
        public int DamagePoints;
        
        protected override void Execute()
        {
            var damageable = Damaging.GetComponent<Damageable>();
            if (!damageable) return;
            
            damageable.Damage(DamagePoints);
        }

        internal override void Cleanup()
        {
            Damaging = null;
            DamagePoints = 0;
        }
    }
}