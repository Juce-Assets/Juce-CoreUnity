﻿using Juce.Core.Repositories;
using Juce.Core.Sequencing.Instructions;
using Juce.CoreUnity.Ui.Frame;
using Juce.CoreUnity.Ui.ViewStack.Context;
using Juce.CoreUnity.Ui.ViewStack.Entries;
using Juce.CoreUnity.Ui.ViewStack.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Ui.ViewStack.Instructions
{
    public class ShowInstruction : Instruction
    {
        private readonly IUiFrame frame;
        private readonly IKeyValueRepository<Type, IViewStackEntry> entriesRepository;
        private readonly ISingleRepository<IViewContext> currentContextRepository;
        private readonly Type entryId;
        private readonly bool instantly;

        public ShowInstruction(
            IUiFrame frame,
            IKeyValueRepository<Type, IViewStackEntry> entriesRepository,
            ISingleRepository<IViewContext> currentContextRepository,
            Type entryId,
            bool instantly
            )
        {
            this.frame = frame;
            this.entriesRepository = entriesRepository;
            this.currentContextRepository = currentContextRepository;
            this.entryId = entryId;
            this.instantly = instantly;
        }

        protected async override Task OnExecute(CancellationToken cancellationToken)
        {
            bool found = entriesRepository.TryGet(entryId, out IViewStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to Show {nameof(IViewStackEntry)} of type {entryId}, " +
                    $"but it was not registered, at {nameof(ShowInstruction)}");
                return;
            }

            ViewStackEntryUtils.SetInteractable(entry, false);

            if (entry.IsPopup)
            {
                bool hasItem = currentContextRepository.TryGet(out IViewContext context);

                if(!hasItem)
                {
                    UnityEngine.Debug.LogError($"Tried to Show {nameof(entry.Id)} as Popup, " +
                        $"but it there was not a main view to attach to, at {nameof(ShowInstruction)}");
                    return;
                }

                context.PopupsViewIds.Add(entry.Id);
            }
            else
            {
                currentContextRepository.Set(new ViewContext(entry.Id));
            }

            ViewStackEntryUtils.Refresh(entry, RefreshType.BeforeShow);

            frame.MoveToForeground(entry.Transform);

            await entry.Visible.SetVisible(visible: true, instantly, cancellationToken);

            ViewStackEntryUtils.Refresh(entry, RefreshType.AfterShow);

            ViewStackEntryUtils.SetInteractable(entry, true);
        }
    }
}
