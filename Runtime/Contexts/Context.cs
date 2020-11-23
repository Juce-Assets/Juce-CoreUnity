using System;
using UnityEngine;

namespace Juce.Core.Contexts
{
    public abstract class Context : MonoBehaviour
    {
        private void Awake()
        {
            Init();
        }

        private void OnDestroy()
        {
            CleanUp();
        }

        protected virtual void Init()
        {
        }

        protected virtual void CleanUp()
        {
        }
    }
}