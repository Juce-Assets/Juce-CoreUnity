using UnityEngine;

namespace Juce.Extensions
{
    public static class Vector3Extensions
    {
        public static readonly Vector3 MaxValue = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        public static readonly Vector3 MinValue = new Vector3(float.MinValue, float.MinValue, float.MaxValue);

        public static Vector3 PerpendicularClockwiseXY(this Vector3 vector3)
        {
            return new Vector3(vector3.y, -vector3.x, vector3.z);
        }

        public static Vector3 PerpendicularCounterClockwiseXY(this Vector3 vector3)
        {
            return new Vector3(-vector3.y, vector3.x, vector3.z);
        }

        public static Vector3 PerpendicularClockwiseXZ(this Vector3 vector3)
        {
            return new Vector3(vector3.z, vector3.y, -vector3.x);
        }

        public static Vector3 PerpendicularCounterClockwiseXZ(this Vector3 vector3)
        {
            return new Vector3(-vector3.z, vector3.y, vector3.x);
        }

        public static Vector3 PerpendicularClockwiseYZ(this Vector3 vector3)
        {
            return new Vector3(vector3.x, vector3.z, -vector3.y);
        }

        public static Vector3 PerpendicularCounterClockwiseYZ(this Vector3 vector3)
        {
            return new Vector3(vector3.x, -vector3.z, vector3.y);
        }

        public static Vector3 Max(this Vector3 vector3, float maxX, float maxY, float maxZ)
        {
            return new Vector3(Mathf.Max(vector3.x, maxX), Mathf.Max(vector3.y, maxY), Mathf.Max(vector3.z, maxZ));
        }
    }
}
