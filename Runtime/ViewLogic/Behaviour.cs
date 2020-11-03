namespace Juce.Core.ViewLogic
{
    public abstract class Behaviour
    {
        private bool enabled;

        public void Enable()
        {
            if (enabled)
            {
                return;
            }

            enabled = true;

            OnEnable();
        }

        public void Disable()
        {
            if (!enabled)
            {
                return;
            }

            enabled = false;

            OnDisable();
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }
    }
}