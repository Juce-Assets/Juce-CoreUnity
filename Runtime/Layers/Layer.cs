using UnityEngine;

namespace Juce.CoreUnity.Layers
{
    [CreateAssetMenu(fileName = "Layer", menuName = "Juce/Layers/Layer", order = 1)]
    public class Layer : ScriptableObject
    {
        [Header("Values")]
        [SerializeField] private int layerValue = default;

        public int LayerValue => layerValue;
    }
}