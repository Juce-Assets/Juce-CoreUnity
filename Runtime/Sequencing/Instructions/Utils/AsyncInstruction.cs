using System;
using System.Threading.Tasks;

namespace Juce.Core.Sequencing
{
    public abstract class AsyncInstruction : Instruction
    {
        protected override void OnStart()
        {
            OnAsyncStart().RunAsync(() =>
            {
                MarkAsCompleted();
            });
        }

        protected abstract Task OnAsyncStart();
    }
}