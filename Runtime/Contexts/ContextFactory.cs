using Juce.Core.Di.Builder;
using Juce.Core.Di.Container;
using Juce.Core.Disposables;
using Juce.CoreUnity.Service;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.CoreUnity.Contexts
{
    public class ContextFactory<TInteractor, TInstance> : IContextFactory<TInteractor, TInstance> where TInstance : MonoBehaviour
    {
        private readonly string contextSceneName;
        private readonly IContextInstaller<TInstance> contextInstaller;

        public ContextFactory(
            string contextSceneName,
            IContextInstaller<TInstance> contextInstaller
            )
        {
            this.contextSceneName = contextSceneName;
            this.contextInstaller = contextInstaller;
        }

        public async Task<ITaskDisposable<TInteractor>> Create(params IDiContainer[] parentContainers)
        {
            TInstance contextInstance = await ContextInstanceLoader.Load<TInstance>(contextSceneName);

            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind(parentContainers);

            contextInstaller.Install(containerBuilder, contextInstance);

            IDiContainer container = containerBuilder.Build();

            TInteractor interactor = container.Resolve<TInteractor>();

            Func<TInteractor, Task> onDispose = (TInteractor _) =>
            {
                ServiceLocator.Unregister<ITaskDisposable<TInteractor>>();

                container.Dispose();

                return ContextInstanceLoader.Unload(contextSceneName);
            };

            ITaskDisposable<TInteractor> disposable = new TaskDisposable<TInteractor>(
                interactor,
                onDispose
                );

            ServiceLocator.Register(disposable);

            return disposable;
        }
    }
}
