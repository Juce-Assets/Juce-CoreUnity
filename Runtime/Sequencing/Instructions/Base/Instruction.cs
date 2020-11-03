namespace Juce.Core.Sequencing
{
    public abstract class Instruction
    {
        private bool started;
        private bool finished;

        private bool markedAsCompleted;

        public bool Started => started;
        public bool Finished => finished;
        public bool MarkedAsCompleted => markedAsCompleted;

        public void MarkAsCompleted()
        {
            markedAsCompleted = true;
        }

        public void Start()
        {
            if (started)
            {
                return;
            }

            started = true;

            OnStart();
        }

        public void Update()
        {
            if (finished)
            {
                return;
            }

            OnUpdate();
        }

        public void Finish()
        {
            if (!started || finished)
            {
                return;
            }

            finished = true;
            started = false;

            OnFinish();
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnFinish()
        {
        }
    }
}