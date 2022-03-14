using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.Layers
{
    public class RendererLayerSelector : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private List<Renderer> targets = default;

        [Header("Layer")]
        [SerializeField] private Layer layer = default;

        private void Awake()
        {
            foreach (Renderer target in targets)
            {
                target.sortingOrder = layer.LayerValue;
            }
        }
    }
}