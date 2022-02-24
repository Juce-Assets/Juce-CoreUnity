using Juce.Core.DI.Builder;
using Juce.Core.DI.Container;
using Juce.Core.Disposables;
using Juce.CoreUnity.Service;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.CoreUnity.Contexts
{
    public class ContextFactory<TInteractor, TInstance> where TInstance : MonoBehaviour, IContextFactory<TInteractor, TInstance>
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

        public async Task<ITaskDisposable<TInteractor>> Create(params IDIContainer[] parentContainers)
        {
            TInstance contextInstance = await ContextInstanceLoader.Load<TInstance>(contextSceneName);

            IDIContainerBuilder containerBuilder = new DIContainerBuilder();

            containerBuilder.Bind(parentContainers);

            contextInstaller.Install(containerBuilder, contextInstance);

            IDIContainer container = containerBuilder.Build();

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
