using UnityEngine;

namespace Mechanics.ScriptableObjects.DamageStrategy
{
    [CreateAssetMenu(menuName = "Damaging/DefinedHpDamageStrategy")]
    public class DefinedHpDamageStrategySO : DamageStrategySO
    {
        [SerializeField] private int maxHealthPoints;
        private int currentHealthPoints;

        public override void Initialize()
        {
            currentHealthPoints = maxHealthPoints;
        }

        public override bool Damage(int damagePoints)
        {
            currentHealthPoints -= damagePoints;
            return currentHealthPoints <= 0;
        }
    }
}