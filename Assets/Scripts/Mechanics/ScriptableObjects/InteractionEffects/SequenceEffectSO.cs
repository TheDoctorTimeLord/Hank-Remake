using UnityEngine;

namespace Mechanics.ScriptableObjects.InteractionEffects
{
    [CreateAssetMenu(menuName = "GameFieldColliding/Effects/EffectsSequence")]
    public class SequenceEffectSO : InteractionEffectSO
    {
        [SerializeField] private InteractionEffectSO[] effects;
        
        public override void InteractionWith(GameObject interacted, GameObject with)
        {
            if (effects == null) return;

            foreach (var effect in effects)
            {
                effect.InteractionWith(interacted, with);
            }
        }
    }
}