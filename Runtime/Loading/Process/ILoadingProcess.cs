using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Loading.Process
{
    public interface ILoadingProcess
    {
        event Action OnCompleted;

        ILoadingProcess NewChild();

        Task StartLoading(CancellationToken cancellationToken);
        Task CompletedLoading(CancellationToken cancellationToken);
    }
}
