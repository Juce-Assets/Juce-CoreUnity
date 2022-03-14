using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.Layers
{
    public class ParticleSystemLayerSelector : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private List<ParticleSystem> targets = default;

        [Header("Layer")]
        [SerializeField] private Layer layer = default;

        private void Awake()
        {
            foreach (ParticleSystem target in targets)
            {
                ParticleSystemRenderer renderer = target.gameObject.GetComponent<ParticleSystemRenderer>();

                renderer.sortingOrder = layer.LayerValue;
            }
        }
    }
}