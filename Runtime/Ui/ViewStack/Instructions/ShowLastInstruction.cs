﻿using Juce.Core.Repositories;
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
    public class ShowLastInstruction : Instruction
    {
        private readonly IUiFrame frame;
        private readonly IKeyValueRepository<Type, IViewStackEntry> entriesRepository;
        private readonly ISingleRepository<IViewContext> currentContextRepository;
        private readonly Stack<Type> viewStackQueue;
        private readonly bool behindForeground;
        private readonly bool instantly;

        public ShowLastInstruction(
            IUiFrame frame,
            IKeyValueRepository<Type, IViewStackEntry> entriesRepository,
            ISingleRepository<IViewContext> currentContextRepository,
            Stack<Type> viewStackQueue,
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

        protected async override Task OnExecute(CancellationToken cancellationToken)
        {
            if(viewStackQueue.Count == 0)
            {
                UnityEngine.Debug.LogError($"Tried to Show last entry, but view stack queue is empty. " +
                    $"Maybe you wanted to use HideAndPush at some point, instead of just Hide.");
                return;
            }

            Type entryId = viewStackQueue.Pop();

            bool found = entriesRepository.TryGet(entryId, out IViewStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to Show {nameof(IViewStackEntry)} of type {entryId}, " +
                    $"but it was not registered, at {nameof(ShowLastInstruction)}");

                return;
            }

            ViewStackEntryUtils.SetInteractable(entry, false);

            currentContextRepository.Set(new ViewContext(entry.Id));

            ViewStackEntryUtils.Refresh(entry, RefreshType.BeforeShow);

            if (behindForeground)
            {
                frame.MoveBehindForeground(entry.Transform);
            }
            else
            {
                frame.MoveToBackground(entry.Transform);
            }

            await entry.Visible.SetVisible(visible: true, instantly, cancellationToken);

            ViewStackEntryUtils.Refresh(entry, RefreshType.AfterShow);

            ViewStackEntryUtils.SetInteractable(entry, true);
        }
    }
}
