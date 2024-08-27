using UnityEngine;

namespace Mechanics.ScriptableObjects.PassabilityChecker
{
    public abstract class PassabilityCheckerSO : ScriptableObject
    {
        public abstract bool CanMoveToThisPosition(GameObject moving, GameObject movedTo);
    }
}