using Core;
using Gameplay.PlayerStates.Events;
using UnityEngine;

namespace Mechanics.ScriptableObjects.InteractionEffects
{
    [CreateAssetMenu(fileName = "CollectWeapon", menuName = "GameFieldColliding/Effects/CollectWeapon")]
    public class CollectWeaponEffectSO : InteractionEffectSO
    {
        [SerializeField] private int weaponAmount;
        
        public override void InteractionWith(GameObject interacted, GameObject with)
        {
            var ev = Simulation.Schedule<ChangeWeaponEvent>();
            ev.ChangeWeapon = weaponAmount;
        }
    }
}