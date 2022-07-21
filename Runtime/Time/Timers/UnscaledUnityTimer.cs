using Juce.Core.Time;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Time
{
    public class UnscaledUnityTimer : CallbackTimer
    {
        public UnscaledUnityTimer() 
            : base(() => TimeSpan.FromSeconds(UnityEngine.Time.unscaledTime))
        {
           
        }

        public static ITimer FromStarted()
        {
            ITimer timer = new UnscaledUnityTimer();
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