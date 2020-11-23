using System;

namespace Juce.CoreUnity.Time
{
    public interface ITimer
    {
        ITimeContext TimeContext { get; }
        TimeSpan Time { get; }

        void Start();

        void Reset();

        void Restart();

        bool HasReached(TimeSpan time);
    }
}