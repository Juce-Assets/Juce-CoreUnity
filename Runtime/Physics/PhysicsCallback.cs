using System;
using UnityEngine;

namespace Juce.CoreUnity.Physics
{
    public class PhysicsCallback : MonoBehaviour
    {
        public event Action<CollisionData> OnPhysicsCollisionEnter;

        public event Action<CollisionData> OnPhysicsCollisionStay;

        public event Action<CollisionData> OnPhysicsCollisionExit;

        public event Action<Collision2DData> OnPhysicsCollisionEnter2D;

        public event Action<Collision2DData> OnPhysicsCollisionStay2D;

        public event Action<Collision2DData> OnPhysicsCollisionExit2D;

        public event Action<ColliderData> OnPhysicsTriggerEnter;

        public event Action<ColliderData> OnPhysicsTriggerStay;

        public event Action<ColliderData> OnPhysicsTriggerExit;

        public event Action<Collider2DData> OnPhysicsTriggerEnter2D;

        public event Action<Collider2DData> OnPhysicsTriggerStay2D;

        public event Action<Collider2DData> OnPhysicsTriggerExit2D;

        private void OnCollisionEnter(Collision collision)
        {
            OnPhysicsCollisionEnter?.Invoke(new CollisionData(this.gameObject, collision));
        }

        private void OnCollisionStay(Collision collision)
        {
            OnPhysicsCollisionStay?.Invoke(new CollisionData(this.gameObject, collision));
        }

        private void OnCollisionExit(Collision collision)
        {
            OnPhysicsCollisionExit?.Invoke(new CollisionData(this.gameObject, collision));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnPhysicsCollisionEnter2D?.Invoke(new Collision2DData(this.gameObject, collision));
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            OnPhysicsCollisionStay2D?.Invoke(new Collision2DData(this.gameObject, collision));
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            OnPhysicsCollisionExit2D?.Invoke(new Collision2DData(this.gameObject, collision));
        }

        private void OnTriggerEnter(Collider collision)
        {
            OnPhysicsTriggerEnter?.Invoke(new ColliderData(this.gameObject, collision));
        }

        private void OnTriggerStay(Collider collision)
        {
            OnPhysicsTriggerStay?.Invoke(new ColliderData(this.gameObject, collision));
        }

        private void OnTriggerExit(Collider collision)
        {
            OnPhysicsTriggerExit?.Invoke(new ColliderData(this.gameObject, collision));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnPhysicsTriggerEnter2D?.Invoke(new Collider2DData(this.gameObject, collision));
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            OnPhysicsTriggerStay2D?.Invoke(new Collider2DData(this.gameObject, collision));
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            OnPhysicsTriggerExit2D?.Invoke(new Collider2DData(this.gameObject, collision));
        }
    }
}