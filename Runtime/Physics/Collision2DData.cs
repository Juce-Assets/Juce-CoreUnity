using UnityEngine;

namespace Juce.Core.Physics
{
    public class Collision2DData
    {
        public GameObject Owner { get; }
        public Collision2D Collision2D { get; }

        public Collision2DData(GameObject owner, Collision2D collision2d)
        {
            Owner = owner;
            Collision2D = collision2d;
        }
    }
}