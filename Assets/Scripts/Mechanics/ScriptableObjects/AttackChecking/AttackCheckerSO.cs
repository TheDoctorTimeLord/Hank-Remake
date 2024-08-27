using UnityEngine;

namespace Mechanics.ScriptableObjects.AttackChecking
{
    public abstract class AttackCheckerSO : ScriptableObject
    {
        public abstract void Initialize();
        public abstract bool CanAttack();
    }
}