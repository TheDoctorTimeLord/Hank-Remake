using UnityEngine;

namespace Mechanics.ScriptableObjects.PassabilityChecker
{
    [CreateAssetMenu(fileName = "AlwaysStop", menuName = "GameFieldColliding/Checking/AlwaysStop")]
    public class AlwaysStopPassabilityCheckerSO : PassabilityCheckerSO
    {
        public override bool CanMoveToThisPosition(GameObject moving, GameObject movedTo)
        {
            return false;
        }
    }
}