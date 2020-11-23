using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Juce.Utils.Contracts;

namespace Juce.Core.Scenes
{
    public class ScenesLoader
    {
        private readonly List<string> scenes = new List<string>();

        public ScenesLoader(IReadOnlyList<string> scenes)
        {
            this.scenes.AddRange(scenes);
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

                Contract.IsNotNull(loadedScene, $"There was an error loading scene: {scene}. Loaded scene is null");

                Contract.IsTrue(loadedScene.IsValid(), $"There was an error loading scene: {scene}. Loaded scene is invalid");

                taskCompletionSource.SetResult(true);
            });

            return await taskCompletionSource.Task;
        }

        private async Task<bool> UnloadSceneAsync(string scene)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            Scene loadedScene = SceneManager.GetSceneByName(scene);

            Contract.IsNotNull(loadedScene, $"There was an error unloading scene: {scene}. Scene to unload is null");

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