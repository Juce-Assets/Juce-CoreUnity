using System;
using UnityEngine;

namespace Juce.CoreUnity.TextProcessors
{
    public abstract class TextProcessorLayer : MonoBehaviour
    {
        public event Action OnRefresh;

        public void Refresh()
        {
            OnRefresh?.Invoke();
        }

        public abstract string Process(string text);
    }
}