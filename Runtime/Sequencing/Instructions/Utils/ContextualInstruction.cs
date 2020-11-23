using System;

namespace Juce.CoreUnity.Sequencing
{
    public class ContextualInstruction : Instruction
    {
        private readonly Func<Instruction> context;

        private Instruction instruction;

        public ContextualInstruction(Func<Instruction> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException($"{nameof(Func<Instruction>)} is null at {nameof(ContextualInstruction)}");
            }

            this.context = context;
        }

        protected override void OnStart()
        {
            instruction = context.Invoke();

            if (instruction == null)
            {
                MarkAsCompleted();
                return;
            }

            instruction.Start();
        }

        protected override void OnUpdate()
        {
            if (instruction == null)
            {
                return;
            }

            instruction.Update();
        }

        protected override void OnFinish()
        {
            if (instruction == null)
            {
                return;
            }

            instruction.Finish();
        }
    }
}