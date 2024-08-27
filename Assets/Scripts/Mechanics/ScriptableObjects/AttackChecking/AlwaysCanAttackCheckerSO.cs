using UnityEngine;

namespace Mechanics.ScriptableObjects.AttackChecking
{
    [CreateAssetMenu(fileName = "AlwaysCanAttack", menuName = "AttackChecker/AlwaysCanAttack")]
    public class AlwaysCanAttackCheckerSO : AttackCheckerSO
    {
        public override void Initialize()
        {
        }

        public override bool CanAttack()
        {
            return true;
        }
    }
}