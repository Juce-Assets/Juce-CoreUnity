using System;
using Juce.Utils.Contracts;

namespace Juce.Core.Sequencing
{
    public class ContextualInstruction : Instruction
    {
        private readonly Func<Instruction> context;

        private Instruction instruction;

        public ContextualInstruction(Func<Instruction> context)
        {
            Contract.IsNotNull(context);

            this.context = context;
        }

        protected override void OnStart()
        {
            instruction = context.Invoke();

            if(instruction == null)
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
