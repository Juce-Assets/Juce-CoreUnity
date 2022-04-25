using Juce.Core.Logging;

namespace Juce.CoreUnity.Logging
{
    public class OwnedUnityLoggerOutput : ILoggerOutput
    {
        private readonly ILoggerOwner owner;

        public OwnedUnityLoggerOutput(ILoggerOwner owner)
        {
            this.owner = owner;
        }

        public void Output(string output)
        {
            string finalString = string.Format("[{0}] {1}", owner.Name, output);

            UnityEngine.Debug.Log(finalString);
        }
    }
}
