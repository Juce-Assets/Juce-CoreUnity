using UnityEngine;

namespace Juce.CoreUnity
{
    public static class GizmosUtils
    {
        public static void DrawLine(Vector3 from, Vector3 to, Color color)
        {
            Color lastColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawLine(from, to);
            Gizmos.color = lastColor;
        }
    }
}