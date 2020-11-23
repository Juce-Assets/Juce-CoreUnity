namespace Juce.CoreUnity.Shared
{
    public class SharedObject
    {
        public string UID { get; }

        public SharedObject()
        {
            UID = System.Guid.NewGuid().ToString();
        }
    }
}