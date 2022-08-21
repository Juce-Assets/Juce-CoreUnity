using Juce.Core.Di.Container;
using Juce.Core.Di.Contexts;
using Juce.Core.Di.Extensions;
using Juce.Core.Di.Installers;
using Juce.Core.Disposables;
using Juce.CoreUnity.Extensions;
using Juce.CoreUnity.SceneManagement.Loader;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Juce.CoreUnity.Di.Contexts
{
    public sealed class SceneDiContext<TResult, TMonoBehaviourInstance> : IAsyncDiContext<TResult> 
        where TMonoBehaviourInstance : MonoBehaviour
    {
        private readonly string sceneName;
        private readonly bool setAsActiveScene;
        private readonly IInstaller[] installers;

        public SceneDiContext(
            string sceneName,
            bool setAsActiveScene = false,
            params IInstaller[] installers
            )
        {
            this.sceneName = sceneName;
            this.setAsActiveScene = setAsActiveScene;
            this.installers = installers;
        }

        public async Task<IAsyncDisposable<TResult>> Install()
        {
            SceneLoadResult sceneLoadResult = await RuntimeSceneLoader.LoadFromName(
                sceneName,
                LoadSceneMode.Additive,
                setAsActiveScene
                );


            if (!sceneLoadResult.Success)
            {
                throw new System.Exception($"Scene {sceneName} could not be loaded for {nameof(SceneDiContext<TResult, TMonoBehaviourInstance>)}");
            }

            bool instanceFound = sceneLoadResult.Scene.TryGetRootComponent(out TMonoBehaviourInstance instance);

            if(!instanceFound)
            {
                throw new System.Exception($"{nameof(TMonoBehaviourInstance)} instance not found for {nameof(SceneDiContext<TResult, TMonoBehaviourInstance>)}");
            }

            List<IInstaller> allInstallers = new List<IInstaller>();
            allInstallers.AddRange(installers);
            allInstallers.Add(new CallbackInstaller(c => c.Bind<TMonoBehaviourInstance>().FromInstance(instance)));

            IDiContainer container = DiContainerBuilderExtensions.BuildFromInstallers(allInstallers);

            Task Dispose(TResult result)
            {
                container.Dispose();

                return RuntimeSceneLoader.UnloadFromName(sceneName);
            }

            TResult result = container.Resolve<TResult>();

            return new CallbackAsyncDisposable<TResult>(
                result,
                Dispose
            );
        }
    }
}
