using System;

namespace Juce.CoreUnity.Time
{
    public class Timer : ITimer
    {
        private bool started;
        private TimeSpan startTime;

        public ITimeContext TimeContext { get; private set; }

        public TimeSpan Time
        {
            get
            {
                if (!started)
                {
                    return TimeSpan.Zero;
                }

                return TimeContext.Time - startTime;
            }
        }

        public Timer(TickableTimeContext tickableTimeContext)
        {
            if (tickableTimeContext == null)
            {
                throw new ArgumentNullException($"Null {nameof(TickableTimeContext)} at {nameof(Timer)}");
            }

            TimeContext = tickableTimeContext;
        }

        public void Start()
        {
            if (started)
            {
                return;
            }

            started = true;

            startTime = TimeContext.Time;
        }

        public void Reset()
        {
            started = false;

            startTime = TimeSpan.Zero;
        }

        public void Restart()
        {
            Reset();
            Start();
        }

        public bool HasReached(TimeSpan timeSpan)
        {
            return TimeSpan.Compare(timeSpan, Time) != -1;
        }
    }
}