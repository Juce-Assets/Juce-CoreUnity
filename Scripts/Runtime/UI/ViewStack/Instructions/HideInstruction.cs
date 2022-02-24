﻿using Juce.Core.Repositories;
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
    public class HideInstruction : Instruction
    {
        private readonly IUiFrame frame;
        private readonly IKeyValueRepository<Type, IViewStackEntry> entriesRepository;
        private readonly ISingleRepository<IViewContext> currentContextRepository;
        private readonly Queue<Type> viewStackQueue;
        private readonly Type entryId;
        private readonly bool pushToViewQueue;
        private readonly bool instantly;

        public HideInstruction(
            IUiFrame frame,
            IKeyValueRepository<Type, IViewStackEntry> entriesRepository,
            ISingleRepository<IViewContext> currentContextRepository,
            Queue<Type> viewStackQueue,
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

            bool hasCurrentContext = currentContextRepository.TryGet(out IViewContext context);

            if (!hasCurrentContext)
            {
                UnityEngine.Debug.LogError($"Tried to Hide {nameof(entry.Id)} as Popup, " +
                    $"but it there was not a current view context, at {nameof(HideInstruction)}");
            }

            if (entry.IsPopup)
            {
                HandlePopup(context);
            }
            else
            {
                await HandleNonPopup(context, cancellationToken);
            }

            await entry.Visible.SetVisible(visible: false, instantly, cancellationToken);
        }

        private void HandlePopup(IViewContext context)
        {
            bool pupupFound = context.PopupsViewIds.Contains(entryId);

            if (!pupupFound)
            {
                UnityEngine.Debug.LogError($"Tried to Hide {entryId} as Popup, " +
                    $"but it was not showing on the first place, at {nameof(HideInstruction)}");
                return;
            }

            context.PopupsViewIds.Remove(entryId);
        }

        private Task HandleNonPopup(IViewContext context, CancellationToken cancellationToken)
        {
            currentContextRepository.Clear();

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

                hideTasks.Add(popupEntry.Visible.SetVisible(visible: false, instantly, cancellationToken));
            }

            if (pushToViewQueue)
            {
                viewStackQueue.Enqueue(entryId);
            }

            return Task.WhenAll(hideTasks);
        }
    }
}
