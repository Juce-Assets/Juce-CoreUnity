using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.Layers
{
    public class CanvasLayerSelector : LayerSelector
    {
        [Header("Target")]
        [SerializeField] private List<Canvas> targets = default;

        [Header("Layer")]
        [SerializeField] private Layer layer = default;

        public override void SetLayer()
        {
            foreach (Canvas target in targets)
            {
                target.sortingOrder = layer.LayerValue;
            }
        }
    }
}