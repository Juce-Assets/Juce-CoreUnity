using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Time
{
    public class UnscaledUnityTimer : IUnityTimer
    {
        private bool started;
        private TimeSpan startTime;

        public TimeSpan Time
        {
            get
            {
                if (!started)
                {
                    return TimeSpan.Zero;
                }

                return TimeSpan.FromSeconds(UnityEngine.Time.unscaledTime) - startTime;
            }
        }

        public void Start()
        {
            if (started)
            {
                return;
            }

            started = true;

            startTime = TimeSpan.FromSeconds(UnityEngine.Time.unscaledTime);
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

        public bool HasReached(TimeSpan time)
        {
            if(!started)
            {
                return false;
            }

            return Time >= time;
        }

        public async Task AwaitReach(TimeSpan time, CancellationToken cancellationToken)
        {
            while(!HasReached(time) && !cancellationToken.IsCancellationRequested)
            {
                await Task.Yield();
            }
        }

        public Task AwaitTime(TimeSpan time, CancellationToken cancellationToken)
        {
            TimeSpan timeToReach = Time + time;

            return AwaitReach(timeToReach, cancellationToken);
        }
    }
}