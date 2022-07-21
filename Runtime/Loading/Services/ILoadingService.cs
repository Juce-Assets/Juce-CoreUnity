using Juce.CoreUnity.Loading.Contexts;
using Juce.CoreUnity.Loading.Events;

namespace Juce.CoreUnity.Loading.Services
{
    public interface ILoadingService
    {
        bool IsLoading { get; }

        void AddBeforeLoading(TaskFunctionEvent func);
        void AddAfterLoading(TaskFunctionEvent func);

        ILoadingContext New();
    }
}
