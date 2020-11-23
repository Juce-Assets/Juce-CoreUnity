using System;

namespace Juce.CoreUnity.Logic
{
    public interface ILogicBridge<T>
    {
        event Action<T> OnReceived;

        void Send(T message);
    }
}