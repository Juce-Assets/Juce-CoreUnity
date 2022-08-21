using Juce.CoreUnity.SceneManagement.Loader;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Juce.CoreUnity.SceneManagement.Group.Drawers
{
    public static class OpenCloseDrawer
    {
        public static void Draw(
            SceneGroup sceneGroup
            )
        {
            using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                if (GUILayout.Button($"Open All"))
                {
                    EditorSceneLoader.Open(sceneGroup.SceneCollection, OpenSceneMode.Single);
                }

                if (GUILayout.Button($"Close All"))
                {
                    EditorSceneLoader.Close(sceneGroup.SceneCollection);
                }
            }
        }
    }
}
