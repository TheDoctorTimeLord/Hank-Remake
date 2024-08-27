using Core;
using UnityEngine;

namespace Gameplay.Damaging
{
    public class KillEvent : Simulation.Event<KillEvent>
    {
        public GameObject Killed;
        
        protected override void Execute()
        {
            Object.Destroy(Killed);
        }

        internal override void Cleanup()
        {
            Killed = null;
        }
    }
}