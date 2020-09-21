using System;
using UnityEngine;

namespace Juce.Core.Contexts
{
    public abstract class Context : MonoBehaviour
    {
        public bool IsReady { get; protected set; }

        private void Awake()
        {
            IsReady = false;

            InitContext();
        }

        private void OnDestroy()
        {
            CleanUpContext();
        }

        public void MarkAsReady()
        {
            IsReady = true;
        }

        protected virtual void InitContext()
        {
        
        }

        protected virtual void CleanUpContext()
        {
          
        }
    }
}
