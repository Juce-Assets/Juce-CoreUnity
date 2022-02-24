using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.CoreUnity.Ui
{
    public abstract class TaskAnimationMonoBehaviour : MonoBehaviour
    {
        public virtual Task Execute(bool instantly, CancellationToken cancellationToken) 
        {
            return Task.CompletedTask;
        }
    }
}
