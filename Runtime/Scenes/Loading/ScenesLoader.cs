using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Juce.CoreUnity.Scenes
{
    public class ScenesLoader
    {
        private readonly IReadOnlyList<string> scenes;

        public ScenesLoader(params string[] scenes)
        {
            this.scenes = scenes;
        }

        public static void SetActiveScene(string scene)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
        }

        public static async Task<SceneLoadResult> LoadScene(string scene, LoadSceneMode mode)
        {
            TaskCompletionSource<SceneLoadResult> taskCompletionSource = new TaskCompletionSource<SceneLoadResult>();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, mode);

            if (asyncLoad == null)
            {
                return new SceneLoadResult(false);
            }

            asyncLoad.completed += ((UnityEngine.AsyncOperation operation) =>
            {
                Scene loadedScene = SceneManager.GetSceneByName(scene);

                if (loadedScene == null)
                {
                    UnityEngine.Debug.LogError($"There was an error loading scene: {scene}. Loaded scene is null at {nameof(ScenesLoader)}");
                }

                if (!loadedScene.IsValid())
                {
                    UnityEngine.Debug.LogError($"There was an error loading scene: {scene}. Loaded scene is not valid at {nameof(ScenesLoader)}");
                }

                taskCompletionSource.SetResult(new SceneLoadResult(true, loadedScene));
            });

            SceneLoadResult result = await taskCompletionSource.Task;

            await Task.Yield();

            return result;
        }

        public static Task<bool> UnloadScene(string scene)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            Scene loadedScene = SceneManager.GetSceneByName(scene);

            if (loadedScene == null || !loadedScene.IsValid())
            {
                UnityEngine.Debug.LogError($"There was an error unloading scene: {scene}. Scene to unload is null {nameof(ScenesLoader)}");
            }

            ParentReminderHelper.TraceBackAllInScene(loadedScene);

            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scene);

            if (asyncUnload == null)
            {
                return Task.FromResult(false);
            }

            asyncUnload.completed += ((UnityEngine.AsyncOperation operation) =>
            {
                taskCompletionSource.SetResult(true);
            });

            return taskCompletionSource.Task;
        }

        public static bool TryFindFirstComponent<T>(
            IReadOnlyList<GameObject> gameObjects, 
            out T component
            ) where T : MonoBehaviour
        {
            foreach(GameObject gameObject in gameObjects)
            {
                T foundComponent = gameObject.GetComponentInChildren<T>();

                if(foundComponent != null)
                {
                    component = foundComponent;
                    return true;
                }
            }

            component = default;
            return false;
        }

        public Task Load()
        {
            Task[] awaitAll = new Task[scenes.Count];

            for (int i = 0; i < scenes.Count; ++i)
            {
                awaitAll[i] = LoadScene(scenes[i], LoadSceneMode.Additive);
            }

            return Task.WhenAll(awaitAll);
        }

        public Task Unload()
        {
            Task[] awaitAll = new Task[scenes.Count];

            for (int i = 0; i < scenes.Count; ++i)
            {
                awaitAll[i] = UnloadScene(scenes[i]);
            }

            return Task.WhenAll(awaitAll);
        }
    }
}