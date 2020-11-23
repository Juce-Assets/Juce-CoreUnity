using UnityEngine;

namespace Juce.CoreUnity.Bootstrap
{
    public abstract class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            Execute();
        }

        protected abstract void Execute();
    }
}