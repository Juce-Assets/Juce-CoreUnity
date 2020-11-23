using System;
using System.Collections.Generic;

namespace Juce.CoreUnity.Sequencing
{
    public class InstructionsSequence : Instruction
    {
        private List<Instruction> instructionQueue = new List<Instruction>();

        private SimultaneousSequenceInstruction currSimultaneousGroupInstruction;

        protected override void OnUpdate()
        {
            InstructionsHelper.AdvanceInstructionsSequentially(ref instructionQueue);

            if (instructionQueue.Count == 0)
            {
                MarkAsCompleted();
            }
        }

        public void Append(Instruction instruction)
        {
            if (Started || Finished)
            {
                return;
            }

            if (instruction == null)
            {
                throw new ArgumentNullException($"Tried to append {nameof(Instruction)} but it was null at {nameof(InstructionsSequence)}");
            }

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

            if (instruction == null)
            {
                throw new ArgumentNullException($"Tried to join {nameof(Instruction)} but it was null at {nameof(InstructionsSequence)}");
            }

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