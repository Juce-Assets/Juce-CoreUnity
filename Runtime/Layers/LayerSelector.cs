using UnityEngine;

namespace Juce.CoreUnity.Layers
{
    public abstract class LayerSelector : MonoBehaviour
    {
        private void Awake()
        {
            SetLayer();
        }

        public abstract void SetLayer();
    }
}