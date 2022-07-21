using UnityEngine;

namespace Juce.CoreUnity.Extensions
{
    public static class Vector2IntExtensions 
    {
        public static readonly Vector2Int MaxValue = new Vector2Int(int.MaxValue, int.MaxValue);
        public static readonly Vector2Int MinValue = new Vector2Int(int.MinValue, int.MinValue);

        public static Vector2Int PerpendicularClockwise(this Vector2Int vector)
        {
            return new Vector2Int(vector.y, -vector.x);
        }

        public static Vector2Int PerpendicularCounterClockwise(this Vector2Int vector)
        {
            return new Vector2Int(-vector.y, vector.x);
        }

        public static Vector2Int Normalized(this Vector2Int vector)
        {
            if (vector.x > 0)
            {
                vector.x = 1;
            }

            if (vector.x < 0)
            {
                vector.x = -1;
            }

            if (vector.y > 0)
            {
                vector.y = 1;
            }

            if (vector.y < 0)
            {
                vector.y = -1;
            }

            return vector;
        }

        public static Vector2Int SwapAxis(this Vector2Int vector)
        {
            return new Vector2Int(vector.y, vector.x);
        }

        public static Vector3 ToVector3XY(this Vector2Int vector)
        {
            return new Vector3(vector.x, vector.y, 0);
        }
    }
}