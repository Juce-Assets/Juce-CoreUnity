using UnityEngine;

namespace Juce.CoreUnity.Contexts
{
    public abstract class Context : MonoBehaviour
    {
        private bool quitting;

        private void Awake()
        {
            Init();
        }

        private void OnDestroy()
        {
            if (quitting)
            {
                return;
            }

            CleanUp();
        }

        private void OnApplicationQuit()
        {
            quitting = true;
        }

        protected abstract void Init();
        protected abstract void CleanUp();
    }
}