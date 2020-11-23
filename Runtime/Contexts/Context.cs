using UnityEngine;

namespace Juce.CoreUnity.Contexts
{
    public abstract class Context : MonoBehaviour
    {
        private void Awake()
        {
            Init();
        }

        private void OnDestroy()
        {
            CleanUp();
        }

        protected abstract void Init();

        protected abstract void CleanUp();
    }
}