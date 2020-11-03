using System;

namespace Juce.Core.Logic
{
    public class LogicBridge<T> : ILogicBridge<T>
    {
        public event Action<T> OnReceived;

        public event Action<T> OnSent;

        public void Receive(T message)
        {
            OnReceived?.Invoke(message);
        }

        public void Send(T message)
        {
            OnSent?.Invoke(message);
        }
    }
}