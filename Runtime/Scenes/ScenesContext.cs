using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Juce.Core.Contracts;

namespace Juce.Core.Scenes
{
    public class ScenesContext
    {
        private readonly List<string> scenes;

        public ScenesContext(List<string> scenes)
        {
            Contract.IsNotNull(scenes);

            this.scenes = scenes;
        }

        private async Task<bool> LoadSceneAsync(string scene, LoadSceneMode mode)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, mode);

            if(asyncLoad == null)
            {
                return false;
            }

            asyncLoad.completed += ((UnityEngine.AsyncOperation operation) =>
            {
                Scene loadedScene = SceneManager.GetSceneByName(scene);

                Contract.IsNotNull(loadedScene, $"There was an error loading scene: {scene}. Loaded scene is null");

                Contract.IsTrue(loadedScene.IsValid(), $"There was an error loading scene: {scene}. Loaded scene is invalid");

                tcs.SetResult(true);
            });

            return await tcs.Task;
        }

        private async Task<bool> UnloadSceneAsync(string scene)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            Scene loadedScene = SceneManager.GetSceneByName(scene);

            Contract.IsNotNull(loadedScene, $"There was an error unloading scene: {scene}. Scene to unload is null");

            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scene);

            if (asyncUnload == null)
            {
                return false;
            }

            asyncUnload.completed += ((UnityEngine.AsyncOperation operation) =>
            {
                tcs.SetResult(true);
            });

            return await tcs.Task;
        }

        public async Task Load()
        {
            for(int i = 0; i < scenes.Count; ++i)
            {
                string currScene = scenes[i];

                await LoadSceneAsync(currScene, LoadSceneMode.Additive);
            }
        }

        public async Task Unload()
        {
            for (int i = 0; i < scenes.Count; ++i)
            {
                string currScene = scenes[i];

                await UnloadSceneAsync(currScene);
            }
        }
    }
}
