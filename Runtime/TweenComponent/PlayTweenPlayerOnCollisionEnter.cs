using Juce.CoreUnity.Physics.Callbacks;
using Juce.TweenComponent;
using UnityEngine;

namespace Juce.CoreUnity.TweenComponent
{
    public class PlayTweenPlayerOnCollisionEnter : MonoBehaviour
    {
        [SerializeField] private PhysicsCallbacks physicsCallbacks = default;
        [SerializeField] private TweenPlayer tweenPlayer = default;

        private void Awake()
        {
            physicsCallbacks.OnPhysicsCollisionEnter += OnPhysicsCollisionEnter;
        }

        private void OnDestroy()
        {
            physicsCallbacks.OnPhysicsCollisionEnter -= OnPhysicsCollisionEnter;
        }

        private void OnPhysicsCollisionEnter(PhysicsCallbacks physicsCallbacks, Collision _)
        {
            tweenPlayer.Play();
        }
    }
}
