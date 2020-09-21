using System;
using System.Collections.Generic;
using Juce.Utils.Contracts;

namespace Juce.Core.Sequencing
{
    public class InstructionsPlayer
    {
        private List<Instruction> instructionQueue = new List<Instruction>();

        public void Play(Instruction instruction)
        {
            Contract.IsNotNull(instruction);

            instructionQueue.Add(instruction);
        }

        public void Update()
        {
            InstructionsHelper.AdvanceInstructionsSequentially(ref instructionQueue);
        }
    }
}
