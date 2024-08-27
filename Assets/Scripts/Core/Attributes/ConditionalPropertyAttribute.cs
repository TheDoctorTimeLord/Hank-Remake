using UnityEngine;

namespace Core.Attributes
{
    public class ConditionalPropertyAttribute : PropertyAttribute {

        public string condition;
        public CheckingType showIf = CheckingType.IfTrue;

        public ConditionalPropertyAttribute(string condition) {
            this.condition = condition;
        }
    }

    public enum CheckingType
    {
        IfTrue,
        IfFalse
    }
}