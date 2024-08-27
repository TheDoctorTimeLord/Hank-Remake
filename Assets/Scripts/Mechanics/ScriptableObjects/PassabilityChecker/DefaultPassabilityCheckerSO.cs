using UnityEngine;

namespace Mechanics.ScriptableObjects.PassabilityChecker
{
    public class DefaultPassabilityCheckerSO : PassabilityCheckerSO
    {
        public override bool CanMoveToThisPosition(GameObject moving, GameObject movedTo)
        {
            return true;
        }
    }
}