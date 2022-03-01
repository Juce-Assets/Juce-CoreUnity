using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.CoreUnity.Bootstraps
{
    public abstract class Bootstrap : MonoBehaviour
    {
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private void Awake()
        {
            ExecuteBootstrap().RunAsync();
        }

        private void OnDestroy()
        {
            TryCancel();
        }

        private async Task ExecuteBootstrap()
        {
            cancellationTokenSource = new CancellationTokenSource();

            await Run(cancellationTokenSource.Token);

            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }

        private void TryCancel()
        {
            if (cancellationTokenSource == null)
            {
                return;
            }

            cancellationTokenSource.Cancel();
        }

        protected abstract Task Run(CancellationToken cancellationToken);
    }
}
