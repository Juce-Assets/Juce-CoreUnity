using System;

namespace Assets.Juce_Core.Runtime.Architecture
{
    public interface IEventDispatcher
    {
        void Subscribe<T>(Action<T> action) where T : new();

        void Dispatch<T>(T ev) where T : new();
    }
}