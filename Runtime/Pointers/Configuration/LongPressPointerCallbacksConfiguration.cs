using UnityEngine;

namespace Juce.CoreUnity.Pointers.Configuration
{
    [CreateAssetMenu(fileName = nameof(LongPressPointerCallbacksConfiguration), menuName = "Juce/Configuration/" + nameof(LongPressPointerCallbacksConfiguration))]
    public class LongPressPointerCallbacksConfiguration : ScriptableObject
    {
        [SerializeField]
        [Tooltip("Delay in seconds it takes for a long press to activate")]
        [Min(0)] private float longPressActivationDelay = default;

        public float LongPressActivationDelay => longPressActivationDelay;
    }
}
