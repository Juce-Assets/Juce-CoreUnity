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