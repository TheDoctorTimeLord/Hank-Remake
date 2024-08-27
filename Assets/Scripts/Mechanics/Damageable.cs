using Core;
using Gameplay.Damaging;
using Mechanics.ScriptableObjects.DamageStrategy;
using UnityEngine;

namespace Mechanics
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] private DamageStrategySO damageStrategy;

        private void Start()
        {
            damageStrategy?.Initialize();
        }

        public void Damage(int damagePoints)
        {
            if (!damageStrategy || !damageStrategy.Damage(damagePoints)) return;
            
            var ev = Simulation.Schedule<KillEvent>();
            ev.Killed = gameObject;
        }
    }
}