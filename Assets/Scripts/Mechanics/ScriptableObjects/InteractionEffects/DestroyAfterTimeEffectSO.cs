using System.Collections;
using Core;
using Gameplay.Damaging;
using UnityEngine;

namespace Mechanics.ScriptableObjects.InteractionEffects
{
    [CreateAssetMenu(menuName = "GameFieldColliding/Effects/DestroydddddddAfterTime")]
    public class DestroyAfterTimeEffectSO : InteractionEffectSO
    {
        [SerializeField] private float timeToDestroy;
        [SerializeField] private bool destroyWith;
        
        public override void InteractionWith(GameObject interacted, GameObject with)
        {
            Simulation.Async.StartCoroutine(DestroyAfterTime(destroyWith ? with : interacted));
        }

        private IEnumerator DestroyAfterTime(GameObject destroyed)
        {
            yield return new WaitForSeconds(timeToDestroy);
            var ev = Simulation.Schedule<KillEvent>();
            ev.Killed = destroyed;
        }
    }
}