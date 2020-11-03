using System;

namespace Juce.Core.Logic
{
    public interface ILogicBridge<T>
    {
        event Action<T> OnReceived;

        void Send(T message);
    }
}