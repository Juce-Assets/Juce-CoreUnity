using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace Juce.CoreUnity.SceneManagement.Scenes
{
    public static class ScenesUtils
    {
        public static List<string> GetAllScenesPaths() 
        {
            string[] scenesGUIDs = AssetDatabase.FindAssets("t:Scene");

            List<string> scenesPaths = scenesGUIDs.Select(AssetDatabase.GUIDToAssetPath).Where(s => s.StartsWith("Assets/")).ToList();

            return scenesPaths;
        }

        public static bool TryGetScenePathFromSceneName(string sceneName, out string foundScenePath)
        {
            List<string> scenesPaths = GetAllScenesPaths();

            foreach(string scenePath in scenesPaths)
            {
                string currentSceneName = Path.GetFileNameWithoutExtension(scenePath);

                bool isScene = string.Equals(currentSceneName, sceneName);

                if(!isScene)
                {
                    continue;
                }

                foundScenePath = scenePath;
                return true;
            }

            foundScenePath = string.Empty;
            return false;
        }
    }
}
