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
            afterLoad.Add(func);
        }

        public void AddBeforeLoading(Func<CancellationToken, Task> func)
        {
            beforeLoad.Add(func);
        }

        public void Enqueue(params Func<CancellationToken, Task>[] functions)
        {
            OnStartLoading();

            foreach (Func<CancellationToken, Task> function in functions)
            {
                sequencer.Play(function);
            }
        }

        public void Enqueue(params Action[] actions)
        {
            OnStartLoading();

            foreach (Action action in actions)
            {
                sequencer.Play(action);
            }
        }

        private void OnStartLoading()
        {
            if(sequencer.Count > 0)
            {
                return;
            }

            IsLoading = true;

            sequencer.OnComplete += OnComplete;

            foreach (Func<CancellationToken, Task> before in beforeLoad)
            {
                sequencer.Play(before.Invoke);
            }
        }

        private void OnComplete()
        {
            sequencer.OnComplete -= OnComplete;

            foreach (Func<CancellationToken, Task> after in afterLoad)
            {
                sequencer.Play(after.Invoke);
            }

            IsLoading = false;
        }
    }
}
