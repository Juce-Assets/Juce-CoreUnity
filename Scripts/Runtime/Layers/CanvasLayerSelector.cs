using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.Layers
{
    public class CanvasLayerSelector : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private List<Canvas> targets = default;

        [Header("Layer")]
        [SerializeField] private Layer layer = default;

        private void Awake()
        {
            foreach (Canvas target in targets)
            {
                target.sortingOrder = layer.LayerValue;
            }
        }
    }
}