using System;
using UnityEngine;

namespace Mechanics.ScriptableObjects
{
    [CreateAssetMenu(menuName = "GameFieldColliding/CollidingType")]
    public class CollidingTypeSO : ScriptableObject
    {
        [SerializeField] private string type;
        public string Type => type;

        public override bool Equals(object other)
        {
            return other is CollidingTypeSO collidingType && Equals(collidingType);
        }

        public bool Equals(CollidingTypeSO other)
        {
            return base.Equals(other) && type == other.type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), type);
        }
    }
}