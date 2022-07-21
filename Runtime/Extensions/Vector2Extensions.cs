using UnityEngine;

namespace Juce.CoreUnity.Extensions
{
    public static class Vector2Extensions
    {
        public static readonly Vector2 MaxValue = new Vector2(float.MaxValue, float.MaxValue);
        public static readonly Vector2 MinValue = new Vector2(float.MinValue, float.MinValue);

        public static Vector2 SwapAxis(this Vector2 vector)
        {
            return new Vector2(vector.y, vector.x);
        }

        public static Vector2 PerpendicularClockwise(this Vector2 vector2)
        {
            return new Vector2(vector2.y, -vector2.x);
        }

        public static Vector2 PerpendicularCounterClockwise(this Vector2 vector2)
        {
            return new Vector2(-vector2.y, vector2.x);
        }

        public static Vector3 ToVector3XY(this Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 0);
        }
    }
}
