using Juce.Core.DI.Container;
using Juce.Core.Disposables;
using UnityEngine;

namespace Juce.CoreUnity.Contexts
{
    public interface IContextInstaller<TInteractor, TInstance> where TInstance : MonoBehaviour
    {
        IDisposable<TInteractor> Install(TInstance instance, params IDIContainer[] parentContainers);
    }
}
