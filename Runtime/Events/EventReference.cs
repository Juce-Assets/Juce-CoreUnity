using System;

namespace Juce.CoreUnity.Events
{
    public class EventReference
    {
        public Type Type { get; }
        public Action<object> Action { get; }

        public EventReference(Type type, Action<object> action)
        {
            Type = type;
            Action = action;
        }
    }
}