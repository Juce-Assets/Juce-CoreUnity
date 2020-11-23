using System;
using UnityEngine;

namespace Juce.Core.Bootstrap
{
    public abstract class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            Execute();
        }

        protected abstract void Execute();
    }
}