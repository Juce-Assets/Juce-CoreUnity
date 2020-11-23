using System;

namespace Juce.CoreUnity.Time
{
    public interface ITimeContext
    {
        event Action<float> OnTimeScaleChanged;

        float TimeScale { get; set; }
        float DeltaTime { get; }
        TimeSpan Time { get; }

        ITimer NewTimer();
    }
}