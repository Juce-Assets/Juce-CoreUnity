using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Loading.Process
{
    public sealed class LoadingProcess : ILoadingProcess
    {
        private readonly IReadOnlyList<Func<CancellationToken, Task>> beforeLoad = Array.Empty<Func<CancellationToken, Task>>();
        private readonly IReadOnlyList<Func<CancellationToken, Task>> afterLoad = Array.Empty<Func<CancellationToken, Task>>();

        public event Action OnCompleted;

        private LoadingProcess()
        {
          
        }

        private LoadingProcess(
            IReadOnlyList<Func<CancellationToken, Task>> beforeLoad,
            IReadOnlyList<Func<CancellationToken, Task>> afterLoad
            )
        {
            this.beforeLoad = beforeLoad;
            this.afterLoad = afterLoad;
        }

        public static ILoadingProcess New(
            IReadOnlyList<Func<CancellationToken, Task>> beforeLoad,
            IReadOnlyList<Func<CancellationToken, Task>> afterLoad
            )
        {
            return new LoadingProcess(beforeLoad, afterLoad);
        }

        public ILoadingProcess NewChild()
        {
            return new LoadingProcess();
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
