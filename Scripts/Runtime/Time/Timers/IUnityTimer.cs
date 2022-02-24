using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Time
{
    public interface IUnityTimer
    {
        TimeSpan Time { get; }

        void Start();
        void Reset();
        void Restart();
        bool HasReached(TimeSpan time);
        Task AwaitReach(TimeSpan time, CancellationToken cancellationToken);
        Task AwaitTime(TimeSpan time, CancellationToken cancellationToken);
    }
}