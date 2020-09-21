using System;
using System.Collections.Generic;
using Juce.Core.Contracts;

namespace Juce.Core.EntryPoint
{
    public abstract class EntryPoint
    {
        private bool executed;

        private readonly List<Action> cleanUpActions = new List<Action>();

        public event Action OnFinish;

        public void Execute()
        {
            if(executed)
            {
                return;
            }

            executed = true;

            cleanUpActions.Clear();

            OnExecute();
        }

        public void CleanUp()
        {
            for(int i = 0; i < cleanUpActions.Count; ++i)
            {
                cleanUpActions[i].Invoke();
            }
        }

        public void AddCleanUpAction(Action action)
        {
            Contract.IsNotNull(action);

            cleanUpActions.Add(action);
        }

        public void Finish()
        {
            if(!executed)
            {
                return;
            }

            OnFinish?.Invoke();
        }

        protected virtual void OnExecute()
        {

        }
    }
}
