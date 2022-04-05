using Juce.Core.Di.Builder;
using UnityEngine;

namespace Juce.CoreUnity.Contexts
{
    public interface IContextInstaller<TContextInstance> where TContextInstance : MonoBehaviour
    {
        void Install(IDiContainerBuilder container, TContextInstance context);
    }
}
