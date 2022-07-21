using UnityEngine;

namespace Juce.CoreUnity.Guizmo.Utils
{
    public static class GizmoUtils
    {
        public static void DrawLine(Vector3 from, Vector3 to, Color color)
        {
#if UNITY_EDITOR
            Color lastColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawLine(from, to);
            Gizmos.color = lastColor;
#endif
        }

        public static void DrawQuadXY(Vector2 center, Vector2 size, Color color)
        {
#if UNITY_EDITOR
            Vector2 halfSize = size * 0.5f;

            var p1 = new Vector3(center.x - halfSize.x, center.y - halfSize.y, 0);
            var p2 = new Vector3(center.x + halfSize.x, center.y - halfSize.y, 0);
            var p3 = new Vector3(center.x + halfSize.x, center.y + halfSize.y, 0);
            var p4 = new Vector3(center.x - halfSize.x, center.y + halfSize.y, 0);

            Color lastColor = Gizmos.color;
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawPolyLine(p1, p2, p3, p4, p1);
            UnityEditor.Handles.color = lastColor;
#endif
        }

        public static void DrawWireDisc(Vector3 center, Vector3 normal, float radius, Color color)
        {
#if UNITY_EDITOR
            Color lastColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawWireDisc(center, normal, radius);
            UnityEditor.Handles.color = lastColor;
#endif
        }

        public static void DrawWireCube(Vector3 center, Vector3 size, Color color)
        {
#if UNITY_EDITOR
            Color lastColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawWireCube(center, size);
            UnityEditor.Handles.color = lastColor;
#endif
        }

        public static void Label(Vector3 center, string text, Color color)
        {
#if UNITY_EDITOR
            Color lastColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.Label(center, text);
            UnityEditor.Handles.color = lastColor;
#endif
        }
    }
}