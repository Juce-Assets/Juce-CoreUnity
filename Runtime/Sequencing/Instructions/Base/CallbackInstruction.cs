using System;

namespace Juce.Core.Sequencing
{
    public class CallbackInstruction : InstantInstruction
    {
        private readonly Action action;

        public CallbackInstruction(Action action)
        {
            this.action = action;
        }

        protected override void OnInstantStart()
        {
            action?.Invoke();
        }
    }
}