using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Time
{
    public class UnscaledUnityTimer : IUnityTimer
    {
        private bool started;
        private float startTime;

        public float Time
        {
            get
            {
                if (!started)
                {
                    return 0.0f;
                }

                return UnityEngine.Time.unscaledTime - startTime;
            }
        }

        public void Start()
        {
            if (started)
            {
                return;
            }

            started = true;

            startTime = UnityEngine.Time.unscaledTime;
        }

        public void Reset()
        {
            started = false;

            startTime = 0.0f;
        }

        public void Restart()
        {
            Reset();
            Start();
        }

        public bool HasReached(float time)
        {
            if(!started)
            {
                return false;
            }

            return Time >= time;
        }

        public async Task AwaitReach(float time, CancellationToken cancellationToken)
        {
            while(!HasReached(time) && !cancellationToken.IsCancellationRequested)
            {
                await Task.Yield();
            }
        }

        public Task AwaitTime(float time, CancellationToken cancellationToken)
        {
            float timeToReach = Time + time;

            return AwaitReach(timeToReach, cancellationToken);
        }
    }
}