using UnityEngine;

namespace Juce.CoreUnity.Layers
{
    public class LayerSelector : MonoBehaviour
    {
        private void Awake()
        {
            SetLayer();
        }

        public virtual void SetLayer() { }
    }
}