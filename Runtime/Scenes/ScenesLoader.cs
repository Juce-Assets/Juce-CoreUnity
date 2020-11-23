using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Juce.CoreUnity.Scenes
{
    public class ScenesLoader
    {
<<<<<<< HEAD
<<<<<<<< HEAD:Runtime/Scenes/ScenesLoader.cs
        private readonly List<string> scenes = new List<string>();

        public ScenesLoader(IReadOnlyList<string> scenes)
        {
            this.scenes.AddRange(scenes);
========
        private readonly IReadOnlyList<string> scenes;

        public ScenesContext(IReadOnlyList<string> scenes)
        {
            this.scenes = scenes;
>>>>>>>> develop:Runtime/Scenes/ScenesContext.cs
=======
        private readonly IReadOnlyList<string> scenes;

        public ScenesLoader(IReadOnlyList<string> scenes)
        {
            scenes = new List<string>(scenes);
>>>>>>> develop
        }

        private async Task<bool> LoadSceneAsync(string scene, LoadSceneMode mode)
        {
<<<<<<< HEAD
<<<<<<<< HEAD:Runtime/Scenes/ScenesLoader.cs
========
            if (scenes == null)
            {
                return false;
            }

>>>>>>>> develop:Runtime/Scenes/ScenesContext.cs
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene, mode);

<<<<<<<< HEAD:Runtime/Scenes/ScenesLoader.cs
            if (asyncLoad == null)
========
            if (asyncOperation == null)
>>>>>>>> develop:Runtime/Scenes/ScenesContext.cs
=======
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, mode);

            if (asyncLoad == null)
>>>>>>> develop
            {
                return false;
            }

<<<<<<< HEAD
            asyncOperation.completed += ((UnityEngine.AsyncOperation operation) =>
=======
            asyncLoad.completed += ((UnityEngine.AsyncOperation operation) =>
>>>>>>> develop
            {
                Scene loadedScene = SceneManager.GetSceneByName(scene);

                if (loadedScene == null)
                {
<<<<<<< HEAD
                    throw new System.Exception($"There was an error loading scene: {scene}. Loaded scene is null at {nameof(ScenesContext)}");
=======
                    throw new System.Exception($"There was an error loading scene: {scene}. Loaded scene is null at {nameof(ScenesLoader)}");
>>>>>>> develop
                }

                if (!loadedScene.IsValid())
                {
<<<<<<< HEAD
                    throw new System.Exception($"There was an error loading scene: {scene}. Loaded scene is invalid at {nameof(ScenesContext)}");
=======
                    throw new System.Exception($"There was an error loading scene: {scene}. Loaded scene is not valid at {nameof(ScenesLoader)}");
>>>>>>> develop
                }

                taskCompletionSource.SetResult(true);
            });

            return await taskCompletionSource.Task;
        }

        private async Task<bool> UnloadSceneAsync(string scene)
        {
<<<<<<< HEAD
<<<<<<<< HEAD:Runtime/Scenes/ScenesLoader.cs
========
            if (scenes == null)
            {
                return false;
            }

>>>>>>>> develop:Runtime/Scenes/ScenesContext.cs
=======
>>>>>>> develop
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            Scene loadedScene = SceneManager.GetSceneByName(scene);

            if (loadedScene == null)
            {
<<<<<<< HEAD
                throw new System.Exception($"There was an error unloading scene: {scene}. Scene to unload is null at {nameof(ScenesContext)}");
=======
                throw new System.Exception($"There was an error unloading scene: {scene}. Scene to unload is null {nameof(ScenesLoader)}");
>>>>>>> develop
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
<<<<<<< HEAD
<<<<<<<< HEAD:Runtime/Scenes/ScenesLoader.cs
            Task[] awaitAll = new Task[scenes.Count];
========
            for (int i = 0; i < scenes.Count; ++i)
            {
                string currScene = scenes[i];
>>>>>>>> develop:Runtime/Scenes/ScenesContext.cs
=======
            Task[] awaitAll = new Task[scenes.Count];
>>>>>>> develop

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