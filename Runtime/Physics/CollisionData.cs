using UnityEngine;

namespace Juce.CoreUnity.Physics
{
    public class CollisionData
    {
        public GameObject Owner { get; }
        public Collision Collision { get; }

        public CollisionData(GameObject owner, Collision collision)
        {
            Owner = owner;
            Collision = collision;
        }
    }
}