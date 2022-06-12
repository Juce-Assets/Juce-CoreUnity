using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Loading.Process
{
    public sealed class LoadingProcess : ILoadingProcess
    {
        public static LoadingProcess Empty = new LoadingProcess();

        private readonly IReadOnlyList<Func<CancellationToken, Task>> beforeLoad = Array.Empty<Func<CancellationToken, Task>>();
        private readonly IReadOnlyList<Func<CancellationToken, Task>> afterLoad = Array.Empty<Func<CancellationToken, Task>>();

        public event Action OnCompleted;

        private LoadingProcess()
        {
          
        }

        public LoadingProcess(
            IReadOnlyList<Func<CancellationToken, Task>> beforeLoad,
            IReadOnlyList<Func<CancellationToken, Task>> afterLoad
            )
        {
            this.beforeLoad = beforeLoad;
            this.afterLoad = afterLoad;
        }

        public async Task StartLoading(CancellationToken cancellationToken)
        {
            foreach(Func<CancellationToken, Task> before in beforeLoad)
            {
                await before?.Invoke(cancellationToken);
            }
        }

        public async Task CompletedLoading(CancellationToken cancellationToken)
        {
            foreach (Func<CancellationToken, Task> after in afterLoad)
            {
                await after?.Invoke(cancellationToken);
            }

            OnCompleted?.Invoke();
        }
    }
}
