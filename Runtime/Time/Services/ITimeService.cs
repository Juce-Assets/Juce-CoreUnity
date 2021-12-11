using Juce.Core.Time;

namespace Juce.CoreUnity.Time
{
    public interface ITimeService 
    {
        ITimeContext UnscaledTimeContext { get; }
        ITimeContext ScaledTimeContext { get; }
    }
}
