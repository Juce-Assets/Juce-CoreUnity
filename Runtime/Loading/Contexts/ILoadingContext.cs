using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Loading.Contexts
{
    public interface ILoadingContext
    {
        ILoadingContext Enqueue(params Func<CancellationToken, Task>[] functions);
        ILoadingContext Enqueue(params Action[] actions);

        ILoadingContext EnqueueAfterLoad(params Action[] actions);

        ILoadingContext ShowInstantly();

        Task Execute(CancellationToken cancellationToken);
        void Execute();
    }
}
