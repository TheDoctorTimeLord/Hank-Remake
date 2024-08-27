using Core;
using Gameplay.Damaging;
using UnityEngine;

namespace Mechanics.ScriptableObjects.InteractionEffects
{
    [CreateAssetMenu(menuName = "GameFieldColliding/Effects/Damage")]
    public class DamageEffectSO : InteractionEffectSO
    {
        [SerializeField] private int damage;
        
        public override void InteractionWith(GameObject interacted, GameObject with)
        {
            var ev = Simulation.Schedule<DamageEvent>();
            ev.Damaging = with;
            ev.DamagePoints = damage;
        }
    }
}