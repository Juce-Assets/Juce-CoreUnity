using Juce.Core.Repositories;
using Juce.Core.Sequencing.Instructions;
using Juce.CoreUnity.Ui.Frame;
using Juce.CoreUnity.Ui.ViewStack.Context;
using Juce.CoreUnity.Ui.ViewStack.Entries;
using Juce.CoreUnity.Ui.ViewStack.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Ui.ViewStack.Instructions
{
    public class HideInstruction : Instruction
    {
        private readonly IUiFrame frame;
        private readonly IKeyValueRepository<Type, IViewStackEntry> entriesRepository;
        private readonly ISingleRepository<IViewContext> currentContextRepository;
        private readonly Stack<Type> viewStackQueue;
        private readonly Type entryId;
        private readonly bool pushToViewQueue;
        private readonly bool instantly;

        public HideInstruction(
            IUiFrame frame,
            IKeyValueRepository<Type, IViewStackEntry> entriesRepository,
            ISingleRepository<IViewContext> currentContextRepository,
            Stack<Type> viewStackQueue,
            Type entryId,
            bool pushToViewQueue,
            bool instantly
            )
        {
            this.frame = frame;
            this.entriesRepository = entriesRepository;
            this.currentContextRepository = currentContextRepository;
            this.viewStackQueue = viewStackQueue;
            this.entryId = entryId;
            this.pushToViewQueue = pushToViewQueue;
            this.instantly = instantly;
        }

        protected override async Task OnExecute(CancellationToken cancellationToken)
        {
            bool found = entriesRepository.TryGet(entryId, out IViewStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to Hide {nameof(IViewStackEntry)} of type {entryId}, " +
                    $"but it was not registered, at {nameof(HideInstruction)}");

                return;
            }

            ViewStackEntryUtils.SetInteractable(entry, false);

            bool hasCurrentContext = currentContextRepository.TryGet(out IViewContext context);

            if (!hasCurrentContext)
            {
                UnityEngine.Debug.LogError($"Tried to Hide {nameof(entry.Id)} as Popup, " +
                    $"but it there was not a current view context, at {nameof(HideInstruction)}");
                return;
            }

            if (entry.IsPopup)
            {
                HandlePopup(context);
            }
            else
            {
                await HandleNonPopup(context, cancellationToken);
            }

            await Hide(entry, instantly, cancellationToken);
        }

        private void HandlePopup(IViewContext context)
        {
            bool pupupFound = context.PopupsViewIds.Contains(entryId);

            if (!pupupFound)
            {
                return;
            }

            context.PopupsViewIds.Remove(entryId);
        }

        private Task HandleNonPopup(IViewContext context, CancellationToken cancellationToken)
        {
            if (context.ViewId == entryId)
            {
                currentContextRepository.Clear();
            }

            List<Task> hideTasks = new List<Task>();

            foreach (Type popupEntryId in context.PopupsViewIds)
            {
                bool popupEntryFound = entriesRepository.TryGet(popupEntryId, out IViewStackEntry popupEntry);

                if (!popupEntryFound)
                {
                    UnityEngine.Debug.LogError($"Tried to Hide {nameof(IViewStackEntry)} of type {entryId}, " +
                        $"but it was not registered, at {nameof(ShowInstruction)}");

                    return Task.CompletedTask; 
                }

                hideTasks.Add(Hide(popupEntry, instantly, cancellationToken));
            }

            if (pushToViewQueue)
            {
                viewStackQueue.Push(entryId);
            }

            return Task.WhenAll(hideTasks);
        }

        private async Task Hide(IViewStackEntry viewStackEntry, bool instantly, CancellationToken cancellationToken)
        {
            ViewStackEntryUtils.Refresh(viewStackEntry, RefreshType.BeforeHide);

            await viewStackEntry.Visible.SetVisible(visible: false, instantly, cancellationToken);

            ViewStackEntryUtils.Refresh(viewStackEntry, RefreshType.AfterHide);
        }
    }
}
