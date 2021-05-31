using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Time
{
    public interface IUnityTimer
    {
        float Time { get; }

        void Start();
        void Reset();
        void Restart();
        bool HasReached(float time);
        Task AwaitReach(float time, CancellationToken cancellationToken);
    }
}