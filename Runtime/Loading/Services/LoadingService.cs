using Juce.Core.Sequencing;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Loading.Services
{
    public class LoadingService : ILoadingService
    {
        private readonly ISequencer sequencer = new Sequencer();

        private readonly List<Func<CancellationToken, Task>> beforeLoad = new List<Func<CancellationToken, Task>>();
        private readonly List<Func<CancellationToken, Task>> afterLoad = new List<Func<CancellationToken, Task>>();

        public bool IsLoading { get; private set; }

        public void AddAfterLoading(Func<CancellationToken, Task> func)
        {
            beforeLoad.Add(func);
        }

        public void AddBeforeLoading(Func<CancellationToken, Task> func)
        {
            afterLoad.Add(func);
        }

        public void EnqueueLoad(params Func<CancellationToken, Task>[] func)
        {
            if(sequencer.Count == 0)
            {
                IsLoading = true;

                sequencer.OnComplete -= OnComplete;
                sequencer.OnComplete += OnComplete;

                foreach (Func<CancellationToken, Task> before in beforeLoad)
                {
                    sequencer.Play(before.Invoke);
                }
            }

            foreach (Func<CancellationToken, Task> toLoad in func)
            {
                sequencer.Play(toLoad);
            }
        }

        private void OnComplete()
        {
            foreach (Func<CancellationToken, Task> after in afterLoad)
            {
                sequencer.Play(after.Invoke);
            }

            IsLoading = false;
        }
    }
}
