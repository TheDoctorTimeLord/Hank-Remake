using Core.Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ConditionalPropertyAttribute))]
    public class ConditionalPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(ShouldShow(property))
                EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (ShouldShow(property))
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }
            return -EditorGUIUtility.standardVerticalSpacing;
        }

        private bool ShouldShow(SerializedProperty property)
        {
            var conditionAttribute = (ConditionalPropertyAttribute)attribute;
            var conditionPath = GetConditionPropertyPath(conditionAttribute, property);
    
            var conditionProperty = property.serializedObject.FindProperty(conditionPath);
            if (conditionProperty is null)
            {
                Debug.LogError($"Property [{conditionPath}] is not found. Check conditions on properties in this class");
                return false;
            }
            if (conditionProperty is not { propertyType: SerializedPropertyType.Boolean })
            {
                Debug.LogError($"Property [{conditionPath}] is not boolean. Supported only boolean type");
            }

            return conditionAttribute.showIf switch
            {
                    CheckingType.IfTrue => conditionProperty.boolValue,
                    CheckingType.IfFalse => !conditionProperty.boolValue,
                    _ => false
            };
        }

        private static string GetConditionPropertyPath(ConditionalPropertyAttribute conditionAttribute, SerializedProperty property)
        {
            var conditionPath = conditionAttribute.condition;
    
            var thisPropertyPath = property.propertyPath;
            var last = thisPropertyPath.LastIndexOf('.');
            
            return last >= 0 ? thisPropertyPath[..(last + 1)] + conditionPath : conditionPath;
        }
    }
}