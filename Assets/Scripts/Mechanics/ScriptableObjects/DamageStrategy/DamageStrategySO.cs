using UnityEngine;

namespace Mechanics.ScriptableObjects.DamageStrategy
{
    public abstract class DamageStrategySO : ScriptableObject
    {
        public abstract void Initialize();
        public abstract bool Damage(int damagePoints);
    }
}