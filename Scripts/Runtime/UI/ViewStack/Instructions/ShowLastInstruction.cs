using Juce.Core.Repositories;
using Juce.Core.Sequencing;
using Juce.CoreUnity.Ui.Frame;
using Juce.CoreUnity.ViewStack;
using Juce.CoreUnity.ViewStack.Context;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Services.ViewStack.Instructions
{
    public class ShowLastInstruction : Instruction
    {
        private readonly IUiFrame frame;
        private readonly IKeyValueRepository<Type, IViewStackEntry> entriesRepository;
        private readonly ISingleRepository<IViewContext> currentContextRepository;
        private readonly Queue<Type> viewStackQueue;
        private readonly bool behindForeground;
        private readonly bool instantly;

        public ShowLastInstruction(
            IUiFrame frame,
            IKeyValueRepository<Type, IViewStackEntry> entriesRepository,
            ISingleRepository<IViewContext> currentContextRepository,
            Queue<Type> viewStackQueue,
            bool behindForeground,
            bool instantly
            )
        {
            this.frame = frame;
            this.entriesRepository = entriesRepository;
            this.currentContextRepository = currentContextRepository;
            this.viewStackQueue = viewStackQueue;
            this.behindForeground = behindForeground;
            this.instantly = instantly;
        }

        protected override Task OnExecute(CancellationToken cancellationToken)
        {
            if(viewStackQueue.Count == 0)
            {
                UnityEngine.Debug.LogError($"Tried to Show last entry, but view stack queue is empty. " +
                    $"Maybe you wanted to use HideAndPush at some point, instead of just Hide.");
                return Task.CompletedTask;
            }

            Type entryId = viewStackQueue.Dequeue();

            bool found = entriesRepository.TryGet(entryId, out IViewStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to Show {nameof(IViewStackEntry)} of type {entryId}, " +
                    $"but it was not registered, at {nameof(ShowLastInstruction)}");

                return Task.CompletedTask;
            }

            currentContextRepository.Set(new ViewContext(entry.Id));

            entry.Refreshable.Refresh();

            if (behindForeground)
            {
                frame.MoveBehindForeground(entry.Transform);
            }
            else
            {
                frame.MoveToBackground(entry.Transform);
            }

            return entry.Visible.SetVisible(visible: true, instantly, cancellationToken);
        }
    }
}
