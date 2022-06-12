using Juce.CoreUnity.Loading.Process;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Loading.Services
{
    public class LoadingService : ILoadingService
    {
        private readonly List<Func<CancellationToken, Task>> beforeLoad = new List<Func<CancellationToken, Task>>();
        private readonly List<Func<CancellationToken, Task>> afterLoad = new List<Func<CancellationToken, Task>>();

        private bool isLoading;

        public void AddAfterLoading(Func<CancellationToken, Task> func)
        {
            beforeLoad.Add(func);
        }

        public void AddBeforeLoading(Func<CancellationToken, Task> func)
        {
            afterLoad.Add(func);
        }

        public bool TryGetNewProcess(out ILoadingProcess loadingProcess)
        {
            if(isLoading)
            {
                loadingProcess = default;
                return false;
            }

            isLoading = true;

            loadingProcess = LoadingProcess.New(beforeLoad, afterLoad);

            loadingProcess.OnCompleted += OnLoadingProcessCompleted;

            return true;
        }

        private void OnLoadingProcessCompleted()
        {
            isLoading = false;
        }
    }
}
