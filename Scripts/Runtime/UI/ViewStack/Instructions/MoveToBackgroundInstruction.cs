using Juce.Core.Repositories;
using Juce.Core.Sequencing;
using Juce.CoreUnity.Ui.Frame;
using Juce.CoreUnity.ViewStack;
using System;

namespace Playground.Services.ViewStack.Instructions
{
    public class MoveToBackgroundInstruction : InstantInstruction
    {
        private readonly IUiFrame frame;
        private readonly IKeyValueRepository<Type, IViewStackEntry> entriesRepository;
        private readonly Type entryId;

        public MoveToBackgroundInstruction(
            IUiFrame frame,
            IKeyValueRepository<Type, IViewStackEntry> entriesRepository,
            Type entryId
            )
        {
            this.frame = frame;
            this.entriesRepository = entriesRepository;
            this.entryId = entryId;
        }

        protected override void OnInstantExecute()
        {
            bool found = entriesRepository.TryGet(entryId, out IViewStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to Show {nameof(IViewStackEntry)} of type {entryId}, " +
                    $"but it was not registered, at {nameof(MoveToBackgroundInstruction)}");

                return;
            }

            frame.MoveToBackground(entry.Transform);
        }
    }
}
