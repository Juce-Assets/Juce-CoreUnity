using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.CoreUnity.UI
{
    public abstract class UIViewAnimation : MonoBehaviour
    {
        public virtual Task Execute(bool instantly, CancellationToken cancellationToken) 
        {
            return Task.CompletedTask;
        }
    }
}
