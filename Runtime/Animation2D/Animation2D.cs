using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.Animation2D
{
    [System.Serializable]
    public sealed class Animation2D
    {
        [SerializeField] private string name = default;
        [SerializeField] private bool loop = default;
        [SerializeField] private float playbackSpeed = default;
        [SerializeField] private List<Sprite> sprites = default;

        public string Name => name;
        public bool Loop => loop;
        public float PlaybackSpeed => playbackSpeed;
        public IReadOnlyList<Sprite> Sprites => sprites;
    }
}