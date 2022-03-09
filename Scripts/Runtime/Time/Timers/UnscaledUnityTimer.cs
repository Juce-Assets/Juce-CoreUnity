using Juce.Core.Time;
using System;

namespace Juce.CoreUnity.Time
{
    public class UnscaledUnityTimer : CallbackTimer
    {
        public UnscaledUnityTimer() 
            : base(() => TimeSpan.FromSeconds(UnityEngine.Time.unscaledTime))
        {
           
        }
    }
}