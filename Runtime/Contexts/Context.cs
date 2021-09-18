using Juce.Core.CleanUp;
using System;
using UnityEngine;

namespace Juce.CoreUnity.Contexts
{
    public abstract class Context : MonoBehaviour
    {
        private readonly ICleanUpActionsRepository cleanUpActionsRepository = new CleanUpActionsRepository();

        private bool quitting;

        private void Awake()
        {
            Init();
        }

        private void OnDestroy()
        {
            if (quitting)
            {
                return;
            }

            cleanUpActionsRepository.CleanUp();
        }

        private void OnApplicationQuit()
        {
            quitting = true;
        }

        protected void AddCleanupAction(Action action)
        {
            cleanUpActionsRepository.Add(action);
        }

        protected abstract void Init();
    }
}