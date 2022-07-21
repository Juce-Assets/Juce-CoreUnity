using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Juce.Core.Extensions;
using Juce.Core.Sequencing.Instructions;
using Juce.Core.Sequencing.Sequences;
using Juce.CoreUnity.Loading.Events;

namespace Juce.CoreUnity.Loading.Contexts
{
    public sealed class LoadingContext : ILoadingContext
    {
        private readonly ISequencer sequencer;

        private readonly IReadOnlyList<TaskFunctionEvent> beforeLoad;
        private readonly IReadOnlyList<TaskFunctionEvent> afterLoad;

        private readonly Queue<IInstruction> enqueuedInstructions = new Queue<IInstruction>();
        private readonly Queue<IInstruction> afterLoadEnqueuedInstructions = new Queue<IInstruction>();

        private bool showInstantly;

        public bool IsLoading { get; private set; }

        public LoadingContext(
            ISequencer sequencer,
            IReadOnlyList<TaskFunctionEvent> beforeLoad,
            IReadOnlyList<TaskFunctionEvent> afterLoad
            )
        {
            this.sequencer = sequencer;
            this.beforeLoad = beforeLoad;
            this.afterLoad = afterLoad;
        }

        public ILoadingContext Enqueue(params Func<CancellationToken, Task>[] functions)
        {
            foreach (Func<CancellationToken, Task> function in functions)
            {
                enqueuedInstructions.Enqueue(new TaskInstruction(function));
            }

            return this;
        }

        public ILoadingContext Enqueue(params Action[] actions)
        {
            foreach (Action action in actions)
            {
                enqueuedInstructions.Enqueue(new ActionInstruction(action));
            }

            return this;
        }

        public ILoadingContext EnqueueAfterLoad(params Action[] actions)
        {
            foreach (Action action in actions)
            {
                afterLoadEnqueuedInstructions.Enqueue(new ActionInstruction(action));
            }

            return this;
        }

        public ILoadingContext ShowInstantly()
        {
            showInstantly = true;

            return this;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            await sequencer.AwaitCompletition();

            IsLoading = true;

            foreach (TaskFunctionEvent before in beforeLoad)
            {
                sequencer.Play(ct => before.Invoke(showInstantly, ct));
            }

            while (enqueuedInstructions.Count > 0)
            {
                sequencer.Play(enqueuedInstructions.Dequeue());
            }

            foreach (TaskFunctionEvent after in afterLoad)
            {
                sequencer.Play(ct => after.Invoke(false, ct));
            }

            while (afterLoadEnqueuedInstructions.Count > 0)
            {
                sequencer.Play(afterLoadEnqueuedInstructions.Dequeue());
            }

            await sequencer.AwaitCompletition();

            IsLoading = false;
        }

        public void Execute()
        {
            Execute(CancellationToken.None).RunAsync();
        }
    }
}
