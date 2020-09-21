using System;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.Core.Animation2D
{
    [System.Serializable]
    public class Animation2D
    {
        [SerializeField] private string name = default;
        [SerializeField] private bool loop = default;
        [SerializeField] private float playback_speed = default;
        [SerializeField] private List<Sprite> sprites = default;

        public string Name => name;
        public bool Loop => loop;
        public float PlaybackSpeed => playback_speed;
        public IReadOnlyList<Sprite> Sprites => sprites;
    }
}
