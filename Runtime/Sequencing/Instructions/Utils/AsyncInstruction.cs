using System.Threading.Tasks;

namespace Juce.CoreUnity.Sequencing
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