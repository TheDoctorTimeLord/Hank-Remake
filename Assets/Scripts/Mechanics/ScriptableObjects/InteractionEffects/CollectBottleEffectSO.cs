using Core;
using Gameplay.PlayerStates.Events;
using UnityEngine;

namespace Mechanics.ScriptableObjects.InteractionEffects
{
    [CreateAssetMenu(fileName = "CollectBottle", menuName = "GameFieldColliding/Effects/CollectBottle")]
    public class CollectBottleEffectSO : InteractionEffectSO
    {
        [SerializeField] private int bottleAmount;
        
        public override void InteractionWith(GameObject interacted, GameObject with)
        {
            var ev = Simulation.Schedule<ChangeBottleEvent>();
            ev.ChangeBottle = bottleAmount;
        }
    }
}