using Juce.Core.Events.Generic;
using UnityEngine;

namespace Juce.CoreUnity.Physics.Callbacks
{
    public class PhysicsCallbacks : MonoBehaviour
    {
        public event GenericEvent<PhysicsCallbacks, Collision> OnPhysicsCollisionEnter;
        public event GenericEvent<PhysicsCallbacks, Collision> OnPhysicsCollisionStay;
        public event GenericEvent<PhysicsCallbacks, Collision> OnPhysicsCollisionExit;

        public event GenericEvent<PhysicsCallbacks, Collision2D> OnPhysicsCollisionEnter2D;
        public event GenericEvent<PhysicsCallbacks, Collision2D> OnPhysicsCollisionStay2D;
        public event GenericEvent<PhysicsCallbacks, Collision2D> OnPhysicsCollisionExit2D;

        public event GenericEvent<PhysicsCallbacks, Collider> OnPhysicsTriggerEnter;
        public event GenericEvent<PhysicsCallbacks, Collider> OnPhysicsTriggerStay;
        public event GenericEvent<PhysicsCallbacks, Collider> OnPhysicsTriggerExit;

        public event GenericEvent<PhysicsCallbacks, Collider2D> OnPhysicsTriggerEnter2D;
        public event GenericEvent<PhysicsCallbacks, Collider2D> OnPhysicsTriggerStay2D;
        public event GenericEvent<PhysicsCallbacks, Collider2D> OnPhysicsTriggerExit2D;

        private void OnCollisionEnter(Collision collision)
        {
            OnPhysicsCollisionEnter?.Invoke(this, collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            OnPhysicsCollisionStay?.Invoke(this, collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            OnPhysicsCollisionExit?.Invoke(this, collision);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnPhysicsCollisionEnter2D?.Invoke(this, collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            OnPhysicsCollisionStay2D?.Invoke(this, collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            OnPhysicsCollisionExit2D?.Invoke(this, collision);
        }

        private void OnTriggerEnter(Collider collision)
        {
            OnPhysicsTriggerEnter?.Invoke(this, collision);
        }

        private void OnTriggerStay(Collider collision)
        {
            OnPhysicsTriggerStay?.Invoke(this, collision);
        }

        private void OnTriggerExit(Collider collision)
        {
            OnPhysicsTriggerExit?.Invoke(this, collision);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnPhysicsTriggerEnter2D?.Invoke(this, collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            OnPhysicsTriggerStay2D?.Invoke(this, collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            OnPhysicsTriggerExit2D?.Invoke(this, collision);
        }
    }
}