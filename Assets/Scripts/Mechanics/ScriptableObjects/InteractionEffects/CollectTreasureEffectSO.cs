using Core;
using Gameplay.PlayerStates.Events;
using UnityEngine;

namespace Mechanics.ScriptableObjects.InteractionEffects
{
    [CreateAssetMenu(fileName = "CollectTreasure", menuName = "GameFieldColliding/Effects/CollectTreasure")]
    public class CollectTreasureEffectSO : InteractionEffectSO
    {
        public override void InteractionWith(GameObject interacted, GameObject with)
        {
            var ev = Simulation.Schedule<ChangeTreasureEvent>();
            ev.ChangeTreasure = 1;
        }
    }
}