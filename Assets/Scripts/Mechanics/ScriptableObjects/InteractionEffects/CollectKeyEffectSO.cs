using Core;
using Gameplay.PlayerStates.Events;
using UnityEngine;

namespace Mechanics.ScriptableObjects.InteractionEffects
{
    [CreateAssetMenu(fileName = "CollectKey", menuName = "GameFieldColliding/Effects/CollectKey")]
    public class CollectKeyEffectSO : InteractionEffectSO
    {
        public override void InteractionWith(GameObject interacted, GameObject with)
        {
            var ev = Simulation.Schedule<ChangeKeysEvent>();
            ev.ChangeKeys = 1;
        }
    }
}