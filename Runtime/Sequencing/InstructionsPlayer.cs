using System.Collections.Generic;

namespace Juce.CoreUnity.Sequencing
{
    public class InstructionsPlayer
    {
        private List<Instruction> instructionQueue = new List<Instruction>();

        public void Play(Instruction instruction)
        {
            if (instruction == null)
            {
                throw new System.ArgumentNullException($"Tried to play null {nameof(Instruction)} at {nameof(InstructionsPlayer)}");
            }

            instructionQueue.Add(instruction);
        }

        public void Update()
        {
            InstructionsHelper.AdvanceInstructionsSequentially(ref instructionQueue);
        }
    }
}