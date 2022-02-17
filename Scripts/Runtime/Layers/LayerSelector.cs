using UnityEngine;

namespace Juce.CoreUnity.Layers
{
    public class LayerSelector : MonoBehaviour
    {
        [Header("Layer")]
        [SerializeField] private Layer layer = default;

        [Header("Targets")]
        [SerializeField] private Renderer targetRenderer = default;

        [SerializeField] private Canvas targetCanvas = default;

        private void Awake()
        {
            SetLayer();
        }

        private void SetLayer()
        {
            if (targetRenderer != null)
            {
                targetRenderer.sortingOrder = layer.LayerValue;
            }

            if (targetCanvas != null)
            {
                targetCanvas.sortingOrder = layer.LayerValue;
            }
        }
    }
}