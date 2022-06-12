using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Loading.Services
{
    public interface ILoadingService
    {
        bool IsLoading { get; }

        void AddBeforeLoading(Func<CancellationToken, Task> func);
        void AddAfterLoading(Func<CancellationToken, Task> func);

        void EnqueueLoad(Func<CancellationToken, Task> func);
    }
}
