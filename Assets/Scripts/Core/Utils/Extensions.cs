using UnityEngine;

namespace Core.Utils
{
    public static class Extensions
    {
        public static T GetOrCreateComponent<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            //Component override operator == and != only. ?? is not overriden
            return component == null ? gameObject.AddComponent<T>() : component;
        }

        public static Vector2 Clamp(this Vector2 vector2, Vector2 minValue, Vector2 maxValue)
        {
            return new Vector2(
                    Mathf.Clamp(vector2.x, minValue.x, maxValue.x),
                    Mathf.Clamp(vector2.y, minValue.y, maxValue.y)
            );
        }
    }
}