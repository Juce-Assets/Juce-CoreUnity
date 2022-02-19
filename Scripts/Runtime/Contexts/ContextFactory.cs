using Juce.Core.DI.Container;
using Juce.Core.Disposables;
using Juce.CoreUnity.Service;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.CoreUnity.Contexts
{
    public class ContextFactory<TInteractor, TInstance> where TInstance : MonoBehaviour
    {
        private readonly string contextSceneName;
        private readonly IContextInstaller<TInteractor, TInstance> contextInstaller;

        public ContextFactory(
            string contextSceneName,
            IContextInstaller<TInteractor, TInstance> contextInstaller
            )
        {
            this.contextSceneName = contextSceneName;
            this.contextInstaller = contextInstaller;
        }

        public async Task<ITaskDisposable<TInteractor>> TryCreate(params IDIContainer[] parentContainers)
        {
            TInstance contextInstance = await ContextInstanceLoader.Load<TInstance>(contextSceneName);

            IDisposable<TInteractor> interactor = contextInstaller.Install(
                contextInstance,
                parentContainers
                );

            Func<TInteractor, Task> onDispose = (TInteractor _) =>
            {
                ServiceLocator.Unregister<ITaskDisposable<TInteractor>>();

                interactor.Dispose();

                return ContextInstanceLoader.Unload(contextSceneName);
            };

            ITaskDisposable<TInteractor> disposable = new TaskDisposable<TInteractor>(
                interactor.Value,
                onDispose
                );

            ServiceLocator.Register(disposable);

            return disposable;
        }
    }
}
