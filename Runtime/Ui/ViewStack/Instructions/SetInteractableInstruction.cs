using Juce.Core.Repositories;
using Juce.Core.Sequencing;
using Juce.CoreUnity.Ui.Frame;
using Juce.CoreUnity.Ui.ViewStack.Entries;
using System;

namespace Juce.CoreUnity.Ui.ViewStack.Instructions
{
    public class SetInteractableInstruction : InstantInstruction
    {
        private readonly IUiFrame frame;
        private readonly IKeyValueRepository<Type, IViewStackEntry> entriesRepository;
        private readonly Type entryId;
        private readonly bool interactable;

        public SetInteractableInstruction(
            IUiFrame frame,
            IKeyValueRepository<Type, IViewStackEntry> entriesRepository,
            Type entryId,
            bool interactable
            )
        {
            this.frame = frame;
            this.entriesRepository = entriesRepository;
            this.entryId = entryId;
            this.interactable = interactable;
        }

        protected override void OnInstantExecute()
        {
            bool found = entriesRepository.TryGet(entryId, out IViewStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to SetInteractable {nameof(IViewStackEntry)} of type {entryId}, " +
                    $"but it was not registered, at {nameof(SetInteractableInstruction)}");
                return;
            }

            ViewStackEntryUtils.SetInteractable(entry, interactable);
        }
    }
}
