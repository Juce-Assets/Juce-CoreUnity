using Juce.Core.DI.Container;
using Juce.Core.Disposables;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.CoreUnity.Contexts
{
    public interface IContextFactory<TInteractor, TInstance> where TInstance : MonoBehaviour
    {
        Task<ITaskDisposable<TInteractor>> Create(params IDIContainer[] parentContainers);
    }
}
