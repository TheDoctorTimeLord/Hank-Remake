using UnityEngine;

namespace Mechanics.ScriptableObjects.InteractionEffects
{
    public abstract class InteractionEffectSO : ScriptableObject
    {
        public abstract void InteractionWith(GameObject interacted, GameObject with);
    }
}