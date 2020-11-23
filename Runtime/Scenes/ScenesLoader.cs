using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Juce.CoreUnity.Scenes
{
    public class ScenesLoader
    {
        private readonly IReadOnlyList<string> scenes;

        public ScenesLoader(IReadOnlyList<string> scenes)
        {
            this.scenes = new List<string>(scenes);
        }

        private async Task<bool> LoadSceneAsync(string scene, LoadSceneMode mode)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, mode);

            if (asyncLoad == null)
            {
                return false;
            }

            asyncLoad.completed += ((UnityEngine.AsyncOperation operation) =>
            {
                Scene loadedScene = SceneManager.GetSceneByName(scene);

                if (loadedScene == null)
                {
                    throw new System.Exception($"There was an error loading scene: {scene}. Loaded scene is null at {nameof(ScenesLoader)}");
                }

                if (!loadedScene.IsValid())
                {
                    throw new System.Exception($"There was an error loading scene: {scene}. Loaded scene is not valid at {nameof(ScenesLoader)}");
                }

                taskCompletionSource.SetResult(true);
            });

            return await taskCompletionSource.Task;
        }

        private async Task<bool> UnloadSceneAsync(string scene)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            Scene loadedScene = SceneManager.GetSceneByName(scene);

            if (loadedScene == null)
            {
                throw new System.Exception($"There was an error unloading scene: {scene}. Scene to unload is null {nameof(ScenesLoader)}");
            }

            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scene);

            if (asyncUnload == null)
            {
                return false;
            }

            asyncUnload.completed += ((UnityEngine.AsyncOperation operation) =>
            {
                taskCompletionSource.SetResult(true);
            });

            return await taskCompletionSource.Task;
        }

        public Task Load()
        {
            Task[] awaitAll = new Task[scenes.Count];

            for (int i = 0; i < scenes.Count; ++i)
            {
                awaitAll[i] = LoadSceneAsync(scenes[i], LoadSceneMode.Additive);
            }

            return Task.WhenAll(awaitAll);
        }

        public Task Unload()
        {
            Task[] awaitAll = new Task[scenes.Count];

            for (int i = 0; i < scenes.Count; ++i)
            {
                awaitAll[i] = UnloadSceneAsync(scenes[i]);
            }

            return Task.WhenAll(awaitAll);
        }
    }
}