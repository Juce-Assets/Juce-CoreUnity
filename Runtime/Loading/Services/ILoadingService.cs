using Juce.Core.Loading.Process;

namespace Juce.CoreUnity.Loading.Services
{
    public interface ILoadingService
    {
        bool IsLoading { get; }

        bool TryStartLoading(out ILoadingProcess loadingProcess);
    }
}
