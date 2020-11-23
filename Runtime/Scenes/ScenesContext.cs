using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Juce.CoreUnity.Scenes
{
    public class ScenesContext
    {
        private readonly IReadOnlyList<string> scenes;

        public ScenesContext(IReadOnlyList<string> scenes)
        {
            this.scenes = scenes;
        }

        private async Task<bool> LoadSceneAsync(string scene, LoadSceneMode mode)
        {
            if (scenes == null)
            {
                return false;
            }

            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene, mode);

            if (asyncOperation == null)
            {
                return false;
            }

            asyncOperation.completed += ((UnityEngine.AsyncOperation operation) =>
            {
                Scene loadedScene = SceneManager.GetSceneByName(scene);

                if (loadedScene == null)
                {
                    throw new System.Exception($"There was an error loading scene: {scene}. Loaded scene is null at {nameof(ScenesContext)}");
                }

                if (!loadedScene.IsValid())
                {
                    throw new System.Exception($"There was an error loading scene: {scene}. Loaded scene is invalid at {nameof(ScenesContext)}");
                }

                taskCompletionSource.SetResult(true);
            });

            return await taskCompletionSource.Task;
        }

        private async Task<bool> UnloadSceneAsync(string scene)
        {
            if (scenes == null)
            {
                return false;
            }

            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            Scene loadedScene = SceneManager.GetSceneByName(scene);

            if (loadedScene == null)
            {
                throw new System.Exception($"There was an error unloading scene: {scene}. Scene to unload is null at {nameof(ScenesContext)}");
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

        public async Task Load()
        {
            for (int i = 0; i < scenes.Count; ++i)
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