using Juce.Core.Time;
using System;

namespace Juce.CoreUnity.Time
{
    public class ScaledUnityTimer : CallbackTimer
    {
        public ScaledUnityTimer()
            : base(() => TimeSpan.FromSeconds(UnityEngine.Time.time))
        {

        }
    }
}