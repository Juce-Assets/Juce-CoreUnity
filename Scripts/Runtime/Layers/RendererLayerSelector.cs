using UnityEngine;

namespace Juce.CoreUnity.Layers
{
    public class RendererLayerSelector : MonoBehaviour
    {
        [Header("Layer")]
        [SerializeField] private Layer layer = default;

        [Header("Target")]
        [SerializeField] private Renderer target = default;

        private void Awake()
        {
            if (target == null)
            {
                return;
            }

            target.sortingOrder = layer.LayerValue;
        }
    }
}