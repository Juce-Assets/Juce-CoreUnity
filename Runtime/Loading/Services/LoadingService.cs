using System.Collections.Generic;
using Juce.Core.Sequencing.Sequences;
using Juce.CoreUnity.Loading.Contexts;
using Juce.CoreUnity.Loading.Events;

namespace Juce.CoreUnity.Loading.Services
{
    public class LoadingService : ILoadingService
    {
        private readonly ISequencer sequencer = new Sequencer();

        private readonly List<TaskFunctionEvent> beforeLoad = new List<TaskFunctionEvent>();
        private readonly List<TaskFunctionEvent> afterLoad = new List<TaskFunctionEvent>();

        public bool IsLoading => sequencer.IsRunning;

        public void AddAfterLoading(TaskFunctionEvent func)
        {
            afterLoad.Add(func);
        }

        public void AddBeforeLoading(TaskFunctionEvent func)
        {
            beforeLoad.Add(func);
        }

        public ILoadingContext New()
        {
            return new LoadingContext(
                sequencer,
                beforeLoad,
                afterLoad
            );
        }
    }
}
