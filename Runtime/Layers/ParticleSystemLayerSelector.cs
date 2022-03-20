using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.Layers
{
    public class ParticleSystemLayerSelector : LayerSelector
    {
        [Header("Target")]
        [SerializeField] private List<ParticleSystem> targets = default;

        [Header("Layer")]
        [SerializeField] private Layer layer = default;

        public override void SetLayer()
        {
            foreach (ParticleSystem target in targets)
            {
                ParticleSystemRenderer renderer = target.gameObject.GetComponent<ParticleSystemRenderer>();

                renderer.sortingOrder = layer.LayerValue;
            }
        }
    }
}