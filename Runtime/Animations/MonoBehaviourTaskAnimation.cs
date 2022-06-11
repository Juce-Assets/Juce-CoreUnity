using Juce.Core.Extensions;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.CoreUnity.Animations
{
    public abstract class MonoBehaviourTaskAnimation : MonoBehaviour, ITaskAnimation
    {
        public void Execute()
        {
            Execute(instantly: false, CancellationToken.None).RunAsync();
        }

        public Task Execute(CancellationToken cancellationToken)
        {
            return Execute(instantly: false, cancellationToken);
        }

        public virtual Task Execute(bool instantly, CancellationToken cancellationToken) 
        {
            return Task.CompletedTask;
        }
    }
}
