namespace Juce.CoreUnity.Sequencing
{
    public abstract class InstantInstruction : Instruction
    {
        protected override void OnStart()
        {
            OnInstantStart();

            MarkAsCompleted();
        }

        protected abstract void OnInstantStart();
    }
}