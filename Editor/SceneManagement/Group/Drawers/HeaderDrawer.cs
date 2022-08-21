using UnityEditor;
using UnityEngine;

namespace Juce.CoreUnity.SceneManagement.Group.Drawers
{
    public static class HeaderDrawer
    {
        public static void Draw(
            SceneGroup sceneGroup
            )
        {
            GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
            style.fontSize = 20;

            GUILayout.Label(sceneGroup.name, style);
        }
    }
}
