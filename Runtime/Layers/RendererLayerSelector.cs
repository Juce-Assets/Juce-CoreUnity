using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.Layers
{
    public class RendererLayerSelector : LayerSelector
    {
        [Header("Target")]
        [SerializeField] private List<Renderer> targets = default;

        [Header("Layer")]
        [SerializeField] private Layer layer = default;

        public override void SetLayer()
        {
            foreach (Renderer target in targets)
            {
                target.sortingOrder = layer.LayerValue;
            }
        }
    }
}