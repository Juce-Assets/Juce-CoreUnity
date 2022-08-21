using Juce.CoreUnity.SceneManagement.Scenes;
using Juce.CoreUnity.SceneManagement.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Juce.CoreUnity.SceneManagement.Reference;

namespace Juce.CoreUnity.SceneManagement.Loader
{
    public static class EditorSceneLoader
    {
        public static bool TryOpenFromReference(SceneReference sceneReference, OpenSceneMode mode, out Scene scene)
        {
            return TryOpenFromPath(sceneReference.ScenePath, mode, out scene);
        }

        public static bool TryOpenFromPath(string scenePath, OpenSceneMode mode, out Scene scene)
        {
            if (string.IsNullOrEmpty(scenePath))
            {
                scene = default;
                return false;
            }

            scene = EditorSceneManager.OpenScene(scenePath, mode);

            if (!scene.IsValid())
            {
                return false;
            }

            return true;
        }

        public static bool TryOpenFromName(string sceneName, OpenSceneMode mode, out Scene scene)
        {
            bool scenePathFound = ScenesUtils.TryGetScenePathFromSceneName(
                sceneName,
                out string scenePath
                );

            if(!scenePathFound)
            {
                scene = default;
                return false;
            }

            return TryOpenFromPath(scenePath, mode, out scene);
        }

        public static List<Scene> Open(ISceneCollection sceneCollection, OpenSceneMode mode)
        {
            List<Scene> ret = new List<Scene>();

            bool first = true;

            foreach (ISceneCollectionEntry sceneEntry in sceneCollection.SceneEntries)
            {
                OpenSceneMode actualMode = OpenSceneMode.Additive;

                if (first)
                {
                    first = false;

                    actualMode = mode;
                }

                bool opened = TryOpenFromPath(sceneEntry.ScenePath, actualMode, out Scene loadedScene);

                if(!opened)
                {
                    UnityEngine.Debug.LogError($"There was an error opening scene {sceneEntry}");
                    continue;
                }

                bool shouldBeSetToActive = sceneEntry.LoadAsActive;

                if (shouldBeSetToActive)
                {
                    SceneManager.SetActiveScene(loadedScene);
                }

                ret.Add(loadedScene);
            }

            return ret;
        }

        public static List<bool> Close(ISceneCollection sceneCollection)
        {
            List<bool> ret = new List<bool>();

            foreach (ISceneCollectionEntry sceneEntry in sceneCollection.SceneEntries)
            {
                Scene scene = SceneManager.GetSceneByPath(sceneEntry.ScenePath);

                if(!scene.IsValid())
                {
                    continue;
                }

                bool result = EditorSceneManager.CloseScene(scene, removeScene: true);

                ret.Add(result);
            }

            return ret;
        }

        public static async Task<SceneLoadResult> Load(string scenePath, LoadSceneMode mode)
        {
            TaskCompletionSource<SceneLoadResult> taskCompletionSource = new TaskCompletionSource<SceneLoadResult>();

            LoadSceneParameters loadParameters = new LoadSceneParameters()
            {
                loadSceneMode = mode
            };

            AsyncOperation asyncLoad = EditorSceneManager.LoadSceneAsyncInPlayMode(scenePath, loadParameters);

            if (asyncLoad == null)
            {
                return new SceneLoadResult();
            }

            asyncLoad.completed += (UnityEngine.AsyncOperation operation) =>
            {
                Scene loadedScene = SceneManager.GetSceneByPath(scenePath);

                if (!loadedScene.IsValid())
                {
                    UnityEngine.Debug.LogError($"There was an error loading scene: {scenePath}. Loaded scene is not valid at {nameof(RuntimeSceneLoader)}");
                }

                taskCompletionSource.SetResult(new SceneLoadResult(loadedScene));
            };

            SceneLoadResult result = await taskCompletionSource.Task;

            await Task.Yield();

            return result;
        }

        public static Task<bool> Unload(string scenePath)
        {
            return RuntimeSceneLoader.UnloadFromPath(scenePath);
        }

        public static async Task<List<SceneLoadResult>> Load(ISceneCollection sceneCollection, LoadSceneMode mode)
        {
            List<SceneLoadResult> ret = new List<SceneLoadResult>();

            bool first = true;

            foreach (ISceneCollectionEntry sceneEntry in sceneCollection.SceneEntries)
            {
                LoadSceneMode actualMode = LoadSceneMode.Additive;

                if (first)
                {
                    first = false;

                    actualMode = mode;
                }

                SceneLoadResult result = await Load(sceneEntry.ScenePath, LoadSceneMode.Additive);

                bool shouldBeSetToActive = sceneEntry.LoadAsActive && result.Success;

                if (shouldBeSetToActive)
                {
                    SceneManager.SetActiveScene(result.Scene);
                }

                ret.Add(result);
            }

            return ret;
        }

        public static Task<List<bool>> Unload(ISceneCollection sceneCollection)
        {
            return RuntimeSceneLoader.Unload(sceneCollection);
        }
    }
}
