using Juce.Core.Loading.Process;

namespace Juce.CoreUnity.Loading.Services
{
    public class LoadingService : ILoadingService
    {
        public bool IsLoading { get; private set; }

        public bool TryStartLoading(out ILoadingProcess loadingProcess)
        {
            if(IsLoading)
            {
                loadingProcess = default;
                return false;
            }

            IsLoading = true;

            loadingProcess = LoadingProcess.New();

            loadingProcess.OnCompleted += OnLoadingProcessCompleted;

            return true;
        }

        private void OnLoadingProcessCompleted()
        {
            IsLoading = false;
        }
    }
}
