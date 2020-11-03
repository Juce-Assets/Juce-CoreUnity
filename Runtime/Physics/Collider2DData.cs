using UnityEngine;

namespace Juce.Core.Physics
{
    public class Collider2DData
    {
        public GameObject Owner { get; }
        public Collider2D Collider2D { get; }

        public Collider2DData(GameObject owner, Collider2D collider2d)
        {
            Owner = owner;
            Collider2D = collider2d;
        }
    }
}