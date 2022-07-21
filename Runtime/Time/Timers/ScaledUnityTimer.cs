using Juce.Core.Time;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Time
{
    public class ScaledUnityTimer : CallbackTimer
    {
        public ScaledUnityTimer()
            : base(() => TimeSpan.FromSeconds(UnityEngine.Time.time))
        {

        }

        public static ITimer FromStarted()
        {
            ITimer timer = new ScaledUnityTimer();
            timer.Start();
            return timer;
        }

        public static Task Await(TimeSpan timeSpan, CancellationToken cancellationToken)
        {
            ITimer timer = FromStarted();
            return timer.AwaitReach(timeSpan, cancellationToken);
        }
    }
}