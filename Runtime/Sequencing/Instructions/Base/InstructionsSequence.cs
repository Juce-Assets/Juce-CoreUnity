using System;
using System.Collections.Generic;
using Juce.Utils.Contracts;

namespace Juce.Core.Sequencing
{
    public class InstructionsSequence : Instruction
    {
        private List<Instruction> instructionQueue = new List<Instruction>();

        private SimultaneousSequenceInstruction currSimultaneousGroupInstruction;

        protected override void OnUpdate()
        {
            InstructionsHelper.AdvanceInstructionsSequentially(ref instructionQueue);

            if(instructionQueue.Count == 0)
            {
                MarkAsCompleted();
            }
        }

        public void Append(Instruction instruction)
        {
            if(Started || Finished)
            {
                return;
            }

            Contract.IsNotNull(instruction);

            currSimultaneousGroupInstruction = new SimultaneousSequenceInstruction();

            currSimultaneousGroupInstruction.Add(instruction);

            instructionQueue.Add(currSimultaneousGroupInstruction);
        }

        public void Join(Instruction instruction)
        {
            if (Started || Finished)
            {
                return;
            }

            Contract.IsNotNull(instruction);

            if (currSimultaneousGroupInstruction == null)
            {
                Append(instruction);
            }
            else
            {
                currSimultaneousGroupInstruction.Add(instruction);
            }
        }

        public void AppendCallback(Action action)
        {
            Append(new CallbackInstruction(action));
        }

        public void JoinCallback(Action action)
        {
            Join(new CallbackInstruction(action));
        }
    }
}
