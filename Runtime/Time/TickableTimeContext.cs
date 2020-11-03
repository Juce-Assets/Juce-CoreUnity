using System;

namespace Juce.Core.Time
{
    public class TickableTimeContext : ITimeContext
    {
        private float timeScale;

        public event Action<float> OnTimeScaleChanged;

        public float DeltaTime { get; private set; }

        public float TimeScale
        {
            get { return timeScale; }

            set
            {
                if (value != timeScale)
                {
                    timeScale = value;

                    OnTimeScaleChanged?.Invoke(timeScale);
                }
            }
        }

        public TimeSpan Time { get; private set; }

        public TickableTimeContext()
        {
            TimeScale = 1.0f;
        }

        public void Tick(float deltaTime)
        {
            DeltaTime = deltaTime * TimeScale;
            Time += TimeSpan.FromSeconds(DeltaTime);
        }

        public ITimer NewTimer()
        {
            return new Timer(this);
        }
    }
}