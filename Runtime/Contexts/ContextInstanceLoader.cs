using Juce.SceneManagement.Loader;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Juce.CoreUnity.Contexts
{
    public static class ContextInstanceLoader
    {
        public static async Task<TContextInstance> Load<TContextInstance>(
            string contextSceneName,
            bool setAsActiveScene = false
            ) where TContextInstance : MonoBehaviour
        {
            SceneLoadResult sceneLoadResult = await RuntimeSceneLoader.LoadFromName(
               contextSceneName,
               LoadSceneMode.Additive,
               setAsActiveScene
               );

            if (!sceneLoadResult.Success)
            {
                throw new System.Exception($"Scene {contextSceneName} could not be loaded for " +
                    $"context");
            }

            bool found = TryFindFirstComponent(
                sceneLoadResult.Scene.GetRootGameObjects(),
                out TContextInstance instance
                );

            if (!found)
            {
                throw new System.Exception($"{typeof(TContextInstance).Name} not found for " +
                    $"context scene {contextSceneName}");
            }

            return instance;
        }

        public static Task Unload(
            string contextSceneName
            )
        {
            return RuntimeSceneLoader.UnloadFromName(contextSceneName);
        }

        private static bool TryFindFirstComponent<T>(
            IReadOnlyList<GameObject> gameObjects,
            out T component
            ) where T : MonoBehaviour
        {
            foreach(GameObject gameObject in gameObjects)
            {
                component = gameObject.GetComponentInChildren<T>();

                if(component != null)
                {
                    return true;
                }
            }

            component = default;
            return false;
        }
    }
}
