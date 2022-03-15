using Juce.Core.Repositories;
using Juce.Core.Sequencing;
using Juce.CoreUnity.Ui.Frame;
using Juce.CoreUnity.ViewStack.Context;
using Juce.CoreUnity.ViewStack.Entries;
using System;

namespace Playground.Services.ViewStack.Instructions
{
    public class CurrentSetInteractableInstruction : InstantInstruction
    {
        private readonly IUiFrame frame;
        private readonly IKeyValueRepository<Type, IViewStackEntry> entriesRepository;
        private readonly ISingleRepository<IViewContext> currentContextRepository;
        private readonly bool interactable;

        public CurrentSetInteractableInstruction(
            IUiFrame frame,
            IKeyValueRepository<Type, IViewStackEntry> entriesRepository,
            ISingleRepository<IViewContext> currentContextRepository,
            bool interactable
            )
        {
            this.frame = frame;
            this.entriesRepository = entriesRepository;
            this.currentContextRepository = currentContextRepository;
            this.interactable = interactable;
        }

        protected override void OnInstantExecute()
        {
            bool entryFound = currentContextRepository.TryGet(
                out IViewContext viewContext
                );

            if(!entryFound)
            {
                return;
            }

            SetInteractable(viewContext.ViewId, interactable);

            foreach (Type popupTypeId in viewContext.PopupsViewIds)
            {
                SetInteractable(popupTypeId, interactable);
            }
        }

        private void SetInteractable(Type typeId, bool interactable)
        {
            bool found = entriesRepository.TryGet(typeId, out IViewStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to SetInteractable {nameof(IViewStackEntry)} of type {typeId}, " +
                    $"but it was not registered, at {nameof(CurrentSetInteractableInstruction)}");

                return;
            }

            ViewStackEntryUtils.SetInteractable(entry, interactable);
        }
    }
}
