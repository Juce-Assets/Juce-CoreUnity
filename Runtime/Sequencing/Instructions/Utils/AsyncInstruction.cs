using System.Threading.Tasks;

namespace Juce.Core.Sequencing
{
    public abstract class AsyncInstruction : Instruction
    {
        protected override void OnStart()
        {
            OnAsyncStart().ExecuteAsync(() =>
            {
                MarkAsCompleted();
            });
        }

        protected abstract Task OnAsyncStart();
    }
}