using Juce.Core.Di.Builder;
using Juce.Core.Di.Container;
using Juce.Core.Disposables;
using Juce.CoreUnity.Service;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.CoreUnity.Contexts
{
    public sealed class ContextFactory<TInteractor, TInstance> : IContextFactory<TInteractor, TInstance> where TInstance : MonoBehaviour
    {
        private readonly string contextSceneName;
        private readonly bool setAsActiveScene;
        private readonly IContextInstaller<TInstance> contextInstaller;

        public ContextFactory(
            string contextSceneName,
            IContextInstaller<TInstance> contextInstaller,
            bool setAsActiveScene = false
            )
        {
            this.contextSceneName = contextSceneName;
            this.contextInstaller = contextInstaller;
            this.setAsActiveScene = setAsActiveScene;
        }

        public async Task<IAsyncDisposable<TInteractor>> Create(params IDiContainer[] parentContainers)
        {
            TInstance contextInstance = await ContextInstanceLoader.Load<TInstance>(contextSceneName, setAsActiveScene);

            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind(parentContainers);

            contextInstaller.Install(containerBuilder, contextInstance);

            IDiContainer container = containerBuilder.Build();

            TInteractor interactor = container.Resolve<TInteractor>();

            Func<TInteractor, Task> onDispose = (TInteractor _) =>
            {
                ServiceLocator.Unregister<IAsyncDisposable<TInteractor>>();

                container.Dispose();

                return ContextInstanceLoader.Unload(contextSceneName);
            };

            IAsyncDisposable<TInteractor> disposable = new AsyncDisposable<TInteractor>(
                interactor,
                onDispose
                );

            ServiceLocator.Register(disposable);

            return disposable;
        }
    }
}
