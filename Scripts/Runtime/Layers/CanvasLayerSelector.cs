using UnityEngine;

namespace Juce.CoreUnity.Layers
{
    public class CanvasLayerSelector : MonoBehaviour
    {
        [Header("Layer")]
        [SerializeField] private Layer layer = default;

        [Header("Target")]
        [SerializeField] private Canvas target = default;

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