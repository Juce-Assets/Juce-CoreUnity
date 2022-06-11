using Juce.Core.Repositories;
using Juce.Core.Sequencing;
using Juce.CoreUnity.Ui.Frame;
using Juce.CoreUnity.Ui.ViewStack.Context;
using Juce.CoreUnity.Ui.ViewStack.Entries;
using System;

namespace Juce.CoreUnity.Ui.ViewStack.Instructions
{
    public class MoveCurrentToForegroundInstruction : InstantInstruction
    {
        private readonly IUiFrame frame;
        private readonly IKeyValueRepository<Type, IViewStackEntry> entriesRepository;
        private readonly ISingleRepository<IViewContext> currentContextRepository;

        public MoveCurrentToForegroundInstruction(
            IUiFrame frame,
            IKeyValueRepository<Type, IViewStackEntry> entriesRepository,
            ISingleRepository<IViewContext> currentContextRepository
            )
        {
            this.frame = frame;
            this.entriesRepository = entriesRepository;
            this.currentContextRepository = currentContextRepository;
        }

        protected override void OnInstantExecute()
        {
            bool hasCurrentContext = currentContextRepository.TryGet(out IViewContext context);

            if (!hasCurrentContext)
            {
                UnityEngine.Debug.LogError($"Tried to MoveCurrentToForeground " +
                    $"but it there was not a current view context, at {nameof(MoveCurrentToForegroundInstruction)}");
                return;
            }

            bool found = entriesRepository.TryGet(context.ViewId, out IViewStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to MoveCurrentToForeground {nameof(IViewStackEntry)} of type {context.ViewId}, " +
                    $"but it was not registered, at {nameof(MoveCurrentToForegroundInstruction)}");

                return;
            }

            frame.MoveToForeground(entry.Transform);
        }
    }
}
