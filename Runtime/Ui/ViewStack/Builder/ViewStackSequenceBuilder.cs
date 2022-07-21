using Juce.Core.Extensions;
using Juce.Core.Repositories;
using Juce.Core.Sequencing.Instructions;
using Juce.Core.Sequencing.Sequences;
using Juce.CoreUnity.Ui.Frame;
using Juce.CoreUnity.Ui.ViewStack.Context;
using Juce.CoreUnity.Ui.ViewStack.Entries;
using Juce.CoreUnity.Ui.ViewStack.Instructions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Juce.CoreUnity.Ui.ViewStack.Builder
{
    public class ViewStackSequenceBuilder : IViewStackSequenceBuilder
    {
        private readonly List<IInstruction> instructionsToPlay = new List<IInstruction>();

        private readonly IUiFrame frame;
        private readonly IKeyValueRepository<Type, IViewStackEntry> entriesRepository;
        private readonly ISingleRepository<IViewContext> currentContextRepository;
        private readonly Stack<Type> viewStackQueue;
        private readonly ISequencer sequencer;

        public ViewStackSequenceBuilder(
            IUiFrame frame,
            IKeyValueRepository<Type, IViewStackEntry> entriesRepository,
            ISingleRepository<IViewContext> currentContextRepository,
            Stack<Type> viewStackQueue,
            ISequencer sequencer
            )
        {
            this.frame = frame;
            this.entriesRepository = entriesRepository;
            this.currentContextRepository = currentContextRepository;
            this.viewStackQueue = viewStackQueue;
            this.sequencer = sequencer;
        }

        public IViewStackSequenceBuilder Show<T>(bool instantly)
        {
            Type entryId = typeof(T);

            instructionsToPlay.Add(new ShowInstruction(
                frame,
                entriesRepository,
                currentContextRepository,
                entryId, 
                instantly
                ));

            return this;
        }

        public IViewStackSequenceBuilder HideAndPush<T>(bool instantly) 
        {
            Type entryId = typeof(T);

            instructionsToPlay.Add(new HideInstruction(
                frame,
                entriesRepository,
                currentContextRepository,
                viewStackQueue,
                entryId,
                pushToViewQueue: true,
                instantly
                ));

            return this;
        }

        public IViewStackSequenceBuilder Hide<T>(bool instantly) 
        {
            Type entryId = typeof(T);

            instructionsToPlay.Add(new HideInstruction(
                frame,
                entriesRepository,
                currentContextRepository,
                viewStackQueue,
                entryId,
                pushToViewQueue: false,
                instantly
                ));

            return this;
        }

        public IViewStackSequenceBuilder ShowLast(bool instantly)
        {
            instructionsToPlay.Add(new ShowLastInstruction(
                frame,
                entriesRepository,
                currentContextRepository,
                viewStackQueue,
                behindForeground: false,
                instantly
                ));

            return this;
        }

        public IViewStackSequenceBuilder ShowLastBehindForeground(bool instantly)
        {
            instructionsToPlay.Add(new ShowLastInstruction(
               frame,
               entriesRepository,
               currentContextRepository,
               viewStackQueue,
               behindForeground: true,
               instantly
               ));

            return this;
        }

        public IViewStackSequenceBuilder MoveToBackground<T>()
        {
            Type entryId = typeof(T);

            instructionsToPlay.Add(new MoveToBackgroundInstruction(
                frame,
                entriesRepository,
                entryId
                ));

            return this;
        }

        public IViewStackSequenceBuilder MoveCurrentToForeground()
        {
            instructionsToPlay.Add(new MoveCurrentToForegroundInstruction(
                frame,
                entriesRepository,
                currentContextRepository
                ));

            return this;
        }

        public IViewStackSequenceBuilder SetInteractable<T>(bool set)
        {
            Type entryId = typeof(T);

            instructionsToPlay.Add(new SetInteractableInstruction(
                frame,
                entriesRepository,
                entryId,
                set
                ));

            return this;
        }

        public IViewStackSequenceBuilder CurrentSetInteractable(bool set)
        {
            instructionsToPlay.Add(new CurrentSetInteractableInstruction(
                frame,
                entriesRepository,
                currentContextRepository,
                set
                ));

            return this;
        }

        public IViewStackSequenceBuilder Callback(Action callback)
        {
            instructionsToPlay.Add(new ActionInstruction(callback));

            return this;
        }

        public Task Execute(CancellationToken cancellationToken)
        {
            foreach(Instruction instruction in instructionsToPlay)
            {
                sequencer.Play(instruction.Execute);
            }

            cancellationToken.Register(sequencer.Kill);

            return sequencer.AwaitCompletition();
        }

        public void Execute()
        {
            Execute(default).RunAsync();
        }
    }
}
