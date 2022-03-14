using UnityEditor;

using UnityEngine;

namespace Juce.CoreUnity.Reorderables
{
    public static class ReorderableHeaderStyles
    {
        private static readonly Color HeaderBackgroundDark = new Color(0.1f, 0.1f, 0.1f, 0.2f);
        private static readonly Color HeaderBackgroundLight = new Color(1f, 1f, 1f, 0.4f);

        private static readonly Color ReorderDark = new Color(1f, 1f, 1f, 0.2f);
        private static readonly Color ReorderLight = new Color(0.1f, 0.1f, 0.1f, 0.2f);

        private static readonly Color ReorderRectDark = new Color(0.8f, 0.8f, 0.8f, 0.5f);
        private static readonly Color ReorderRectLight = new Color(0.2f, 0.2f, 0.2f, 0.5f);

        public static Color HeaderBackgroundColor { get { return EditorGUIUtility.isProSkin ? HeaderBackgroundDark : HeaderBackgroundLight; } }
        public static Color ReorderColor { get { return EditorGUIUtility.isProSkin ? ReorderDark : ReorderLight; } }
        public static Color ReorderRectColor { get { return EditorGUIUtility.isProSkin ? ReorderRectDark : ReorderRectLight; } }

        public static GUIStyle SmallTickbox { get; } = new GUIStyle("ShurikenToggle");
    }
}