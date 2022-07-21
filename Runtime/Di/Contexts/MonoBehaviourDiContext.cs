using Juce.Core.Di.Builder;
using Juce.Core.Di.Container;
using Juce.Core.Di.Contexts;
using Juce.Core.Di.Extensions;
using Juce.Core.Di.Installers;
using Juce.Core.Disposables;
using UnityEngine;

namespace Juce.CoreUnity.Di.Contexts
{
    public abstract class MonoBehaviourDiContext<TResult> : MonoBehaviour, IDiContext<TResult>
    {
        public IDisposable<TResult> Install()
        {
            IDiContainer container = DiContainerBuilderExtensions.BuildFromInstallers(
                new CallbackInstaller(Install)
            );

            void Dispose(TResult result)
            {
                container.Dispose();
            }

            TResult result = container.Resolve<TResult>();

            return new CallbackDisposable<TResult>(
                result,
                Dispose
            );
        }

        protected abstract void Install(IDiContainerBuilder builder);
    }
}
