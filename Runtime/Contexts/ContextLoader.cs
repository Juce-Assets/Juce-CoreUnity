using Juce.CoreUnity.Scenes;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Juce.CoreUnity.Contexts
{
    public static class ContextLoader
    {
        public static async Task<TContextInstance> Load<TContextInstance>(
            string contextSceneName
            ) where TContextInstance : MonoBehaviour
        {
            SceneLoadResult sceneLoadResult = await ScenesLoader.LoadScene(
               contextSceneName,
               LoadSceneMode.Additive
               );

            if(!sceneLoadResult.Success)
            {
                throw new System.Exception($"Scene {contextSceneName} could not be loaded for " +
                    $"context");
            }

            bool found = ScenesLoader.TryFindFirstComponent(
                sceneLoadResult.Scene.GetRootGameObjects(),
                out TContextInstance instance
                );

            if (!found)
            {
                throw new System.Exception($"{nameof(TContextInstance)} not found for " +
                    $"context scene {contextSceneName}");
            }

            return instance;
        }

        public static Task Unload(
            string contextSceneName
            )
        {
            return ScenesLoader.UnloadScene(contextSceneName);
        }
    }
}
