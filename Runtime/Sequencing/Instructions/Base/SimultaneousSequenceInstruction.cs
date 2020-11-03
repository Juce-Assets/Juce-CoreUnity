using Juce.Utils.Contracts;
using System.Collections.Generic;

namespace Juce.Core.Sequencing
{
    public class SimultaneousSequenceInstruction : Instruction
    {
        private List<Instruction> instructionQueue = new List<Instruction>();

        protected override void OnUpdate()
        {
            InstructionsHelper.AdvanceInstructionsSimultaneously(ref instructionQueue);

            if (instructionQueue.Count == 0)
            {
                MarkAsCompleted();
            }
        }

        public void Add(Instruction instruction)
        {
            if (Started || Finished)
            {
                return;
            }

            Contract.IsNotNull(instruction);

            instructionQueue.Add(instruction);
        }
    }
}