using UnityEngine;

namespace Juce.Core.Bootstrap
{
    public abstract class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            Execute();
        }

        protected virtual void Execute()
        {
        }
    }
}