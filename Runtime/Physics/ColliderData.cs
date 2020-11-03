using UnityEngine;

namespace Juce.Core.Physics
{
    public class ColliderData
    {
        public GameObject Owner { get; }
        public Collider Collider { get; }

        public ColliderData(GameObject owner, Collider collider)
        {
            Owner = owner;
            Collider = collider;
        }
    }
}