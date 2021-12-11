using Juce.Core.Time;
using UnityEngine;

namespace Juce.CoreUnity.Time
{
    public class TimeService : MonoBehaviour, ITimeService
    {
        private readonly TickableTimeContext unscaledTimeContext = new TickableTimeContext();
        private readonly TickableTimeContext scaledTimeContext = new TickableTimeContext();

        public ITimeContext UnscaledTimeContext => unscaledTimeContext;
        public ITimeContext ScaledTimeContext => scaledTimeContext;

        public void Update()
        {
            unscaledTimeContext.Tick(UnityEngine.Time.unscaledDeltaTime);
            scaledTimeContext.Tick(UnityEngine.Time.deltaTime);
        }
    }
}
