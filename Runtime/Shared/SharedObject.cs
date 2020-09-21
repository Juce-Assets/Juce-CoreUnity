using System;

namespace Juce.Core.Shared
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
