using System.Collections.Generic;

namespace Juce.CoreUnity.Sequencing
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

            if (instruction == null)
            {
                throw new System.ArgumentNullException($"Tried to add null {nameof(Instruction)} at {nameof(SimultaneousSequenceInstruction)}");
            }

            instructionQueue.Add(instruction);
        }
    }
}