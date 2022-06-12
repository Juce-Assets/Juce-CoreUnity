using Juce.Core.Loading.Process;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Loading.Services
{
    public interface ILoadingService
    {
        void AddBeforeLoading(Func<CancellationToken, Task> func);
        void AddAfterLoading(Func<CancellationToken, Task> func);

        bool TryGetNewProcess(out ILoadingProcess loadingProcess);
    }
}
