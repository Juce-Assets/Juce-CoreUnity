using Juce.Utils.Contracts;
using System;

namespace Juce.Core.Sequencing
{
    public class ConditionalInstruction : Instruction
    {
        private readonly Instruction instruction;
        private readonly Func<bool> context;

        private bool canRun;

        public ConditionalInstruction(Instruction instruction, Func<bool> context)
        {
            Contract.IsNotNull(instruction);
            Contract.IsNotNull(context);

            this.instruction = instruction;
            this.context = context;
        }

        protected override void OnStart()
        {
            canRun = context.Invoke();

            if (!canRun)
            {
                MarkAsCompleted();
                return;
            }

            instruction.Start();
        }

        protected override void OnUpdate()
        {
            if (!canRun)
            {
                return;
            }

            instruction.Update();
        }

        protected override void OnFinish()
        {
            if (!canRun)
            {
                return;
            }

            instruction.Finish();
        }
    }
}