using System;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Juce.CoreUnity.Scenes
{
    public static class ScenesUtils
    {
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