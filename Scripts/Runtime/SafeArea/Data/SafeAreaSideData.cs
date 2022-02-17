using UnityEngine;

namespace Juce.CoreUnity.SafeArea
{
    /// <summary>
    /// Holds the information for one of the sides of the screen, regarding safe area
    /// </summary>
    [System.Serializable]
    public class SafeAreaSideData
    {
        [SerializeField] private bool use = true;
        [SerializeField] [Range(0f, 2f)] private float multiplier = 1.0f;

        public bool Use => use;
        public float Multiplier => multiplier;
    }
}