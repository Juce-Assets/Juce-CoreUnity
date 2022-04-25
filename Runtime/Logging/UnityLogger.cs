using Juce.Core.Logging;

namespace Juce.CoreUnity.Logging
{
    public class UnityLogger : ILoggerOutput
    {
        public static readonly UnityLogger Instance = new UnityLogger();

        private UnityLogger()
        {
            
        }

        public void Output(string output)
        {
            UnityEngine.Debug.Log(output);
        }
    }
}
