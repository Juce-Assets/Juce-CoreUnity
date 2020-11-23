using System;
using System.Collections.Generic;

namespace Juce.CoreUnity.EntryPoint
{
    public abstract class EntryPoint
    {
        private bool executed;

        private readonly List<Action> cleanUpActions = new List<Action>();

        public event Action OnFinish;

        public void Execute()
        {
            if (executed)
            {
                return;
            }

            executed = true;

            cleanUpActions.Clear();

            OnExecute();
        }

        public void Finish()
        {
            if (!executed)
            {
                return;
            }

            OnFinish?.Invoke();
        }

        public void CleanUp()
        {
            for (int i = 0; i < cleanUpActions.Count; ++i)
            {
                cleanUpActions[i].Invoke();
            }
        }

        public void AddCleanUpAction(Action action)
        {
<<<<<<< HEAD
            Contract.IsNotNull(action);
=======
            if (action == null)
            {
                throw new ArgumentNullException($"Null CleanUp action at {nameof(EntryPoint)}");
            }
>>>>>>> develop

            cleanUpActions.Add(action);
        }

        protected abstract void OnExecute();
    }
}