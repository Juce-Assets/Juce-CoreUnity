using Juce.Core.DI.Builder;
using UnityEngine;

namespace Juce.CoreUnity.Contexts
{
    public interface IContextInstaller<TContextInstance> where TContextInstance : MonoBehaviour
    {
        void Install(IDIContainerBuilder container, TContextInstance context);
    }
}
