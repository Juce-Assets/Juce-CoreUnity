using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Juce.CoreUnity.CoreUnity.Scenes
{
    public static class ScenesUtils
    {
        public static string GetScenePathFromName(string sceneName)
        {
            string[] scenes = AssetDatabase.FindAssets("t:Scene");

            foreach (string scene in scenes)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(scene);

                string name = Path.GetFileNameWithoutExtension(assetPath);

                if (string.Equals(name, sceneName))
                {
                    return assetPath;
                }
            }

            return string.Empty;
        }

        public static SceneAsset GetSceneAssetFromName(string sceneName)
        {
            string[] scenes = AssetDatabase.FindAssets("t:Scene");

            foreach (string scene in scenes)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(scene);

                string name = Path.GetFileNameWithoutExtension(assetPath);

                if (string.Equals(name, sceneName))
                {
                    return AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath);
                }
            }

            return null;
        }

        public static void OpenScene(string sceneName)
        {
            string[] scenes = AssetDatabase.FindAssets("t:Scene");

            foreach (string scene in scenes)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(scene);

                string name = Path.GetFileNameWithoutExtension(assetPath);

                if (string.Equals(name, sceneName))
                {
                    EditorSceneManager.OpenScene(assetPath);

                    return;
                }
            }

            UnityEngine.Debug.LogError($"Could not load scene with name: {sceneName}, because it could not be found at {nameof(ScenesUtils)}");
        }
    }
}