using System.Collections.Generic;

namespace Juce.Core.Sequencing
{
    public static class InstructionsHelper
    {
        public static void AdvanceInstructionsSequentially(ref List<Instruction> instructionQueue)
        {
            if (instructionQueue.Count > 0)
            {
                Instruction currInstruction = instructionQueue[0];

                if (!currInstruction.Started && !currInstruction.Finished)
                {
                    currInstruction.Start();
                }

                if (currInstruction.Started && !currInstruction.Finished)
                {
                    currInstruction.Update();
                }

                if (!currInstruction.Finished && currInstruction.MarkedAsCompleted)
                {
                    currInstruction.Finish();

                    instructionQueue.RemoveAt(0);

                    AdvanceInstructionsSequentially(ref instructionQueue);
                }
            }
        }

        public static void AdvanceInstructionsSimultaneously(ref List<Instruction> instructionQueue)
        {
            int finishedCount = 0;

            for (int i = 0; i < instructionQueue.Count; ++i)
            {
                Instruction currInstruction = instructionQueue[i];

                if (!currInstruction.Started && !currInstruction.Finished)
                {
                    currInstruction.Start();
                }

                if (currInstruction.Started && !currInstruction.Finished)
                {
                    currInstruction.Update();
                }

                if (!currInstruction.Finished && currInstruction.MarkedAsCompleted)
                {
                    currInstruction.Finish();
                }

                if (currInstruction.Finished)
                {
                    ++finishedCount;
                }
            }

            if (finishedCount == instructionQueue.Count)
            {
                instructionQueue.Clear();
            }
        }
    }
}