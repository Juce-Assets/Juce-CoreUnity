using Juce.Core.Events.Generic;
using UnityEngine;

namespace Juce.CoreUnity.Physics
{
    public class PhysicsCallbacks : MonoBehaviour
    {
        public event GenericEvent<PhysicsCallbacks, CollisionData> OnPhysicsCollisionEnter;
        public event GenericEvent<PhysicsCallbacks, CollisionData> OnPhysicsCollisionStay;
        public event GenericEvent<PhysicsCallbacks, CollisionData> OnPhysicsCollisionExit;

        public event GenericEvent<PhysicsCallbacks, Collision2DData> OnPhysicsCollisionEnter2D;
        public event GenericEvent<PhysicsCallbacks, Collision2DData> OnPhysicsCollisionStay2D;
        public event GenericEvent<PhysicsCallbacks, Collision2DData> OnPhysicsCollisionExit2D;

        public event GenericEvent<PhysicsCallbacks, ColliderData> OnPhysicsTriggerEnter;
        public event GenericEvent<PhysicsCallbacks, ColliderData> OnPhysicsTriggerStay;
        public event GenericEvent<PhysicsCallbacks, ColliderData> OnPhysicsTriggerExit;

        public event GenericEvent<PhysicsCallbacks, Collider2DData> OnPhysicsTriggerEnter2D;
        public event GenericEvent<PhysicsCallbacks, Collider2DData> OnPhysicsTriggerStay2D;
        public event GenericEvent<PhysicsCallbacks, Collider2DData> OnPhysicsTriggerExit2D;

        private void OnCollisionEnter(Collision collision)
        {
            OnPhysicsCollisionEnter?.Invoke(this, new CollisionData(this.gameObject, collision));
        }

        private void OnCollisionStay(Collision collision)
        {
            OnPhysicsCollisionStay?.Invoke(this, new CollisionData(this.gameObject, collision));
        }

        private void OnCollisionExit(Collision collision)
        {
            OnPhysicsCollisionExit?.Invoke(this, new CollisionData(this.gameObject, collision));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnPhysicsCollisionEnter2D?.Invoke(this, new Collision2DData(this.gameObject, collision));
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            OnPhysicsCollisionStay2D?.Invoke(this, new Collision2DData(this.gameObject, collision));
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            OnPhysicsCollisionExit2D?.Invoke(this, new Collision2DData(this.gameObject, collision));
        }

        private void OnTriggerEnter(Collider collision)
        {
            OnPhysicsTriggerEnter?.Invoke(this, new ColliderData(this.gameObject, collision));
        }

        private void OnTriggerStay(Collider collision)
        {
            OnPhysicsTriggerStay?.Invoke(this, new ColliderData(this.gameObject, collision));
        }

        private void OnTriggerExit(Collider collision)
        {
            OnPhysicsTriggerExit?.Invoke(this, new ColliderData(this.gameObject, collision));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnPhysicsTriggerEnter2D?.Invoke(this, new Collider2DData(this.gameObject, collision));
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            OnPhysicsTriggerStay2D?.Invoke(this, new Collider2DData(this.gameObject, collision));
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            OnPhysicsTriggerExit2D?.Invoke(this, new Collider2DData(this.gameObject, collision));
        }
    }
}