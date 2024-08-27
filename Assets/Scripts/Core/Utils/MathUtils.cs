using System.Drawing;
using UnityEngine;

namespace Core.Utils
{
    public static class MathUtils
    {
        public static float GetIntersectionSquare(Bounds first, Bounds second)
        {
            var x1 = Mathf.Max(first.min.x, second.min.x);
            var y1 = Mathf.Max(first.min.y, second.min.y);
            var x2 = Mathf.Min(first.max.x, second.max.x);
            var y2 = Mathf.Min(first.max.y, second.max.y);

            return x1 > x2 || y1 > y2 ? 0 : (x2 - x1) * (y2 - y1);
        }

        public static float GetIntersectionRatio(Bounds intersectable, Bounds with)
        {
            var intersectionSquare = GetIntersectionSquare(intersectable, with);
            var intersectableSquare = intersectable.size.x * intersectable.size.y;
            return intersectableSquare == 0 ? 0 : intersectionSquare / intersectableSquare;
        }

        public static bool ContainsInBounds(float coordinate, float minCoordinateValue, float maxCoordinateValue)
        {
            return minCoordinateValue <= coordinate && coordinate < maxCoordinateValue;
        }

        public static bool ContainsInBounds(Bounds containsIn, Vector2 checkedPosition)
        {
            var minBounds = containsIn.min;
            var maxBounds = containsIn.max;
            return ContainsInBounds(checkedPosition.x, minBounds.x, maxBounds.x) 
                && ContainsInBounds(checkedPosition.y, minBounds.y, maxBounds.y);
        }

        public static bool ContainsInBounds(Vector2 position, Vector2 minBoundsVertex, Vector2 maxBoundsVertex)
        {
            return ContainsInBounds(position.x, minBoundsVertex.x, maxBoundsVertex.x)
                && ContainsInBounds(position.y, minBoundsVertex.y, maxBoundsVertex.y);
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}