using Juce.CoreUnity.PointerCallback;
using Juce.TweenComponent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Juce.CoreUnity.TweenComponent
{
    public class PointerCallbacksTweenPlayer : MonoBehaviour
    {
        [Header("Pointer Callbacks")]
        [SerializeField] private PointerCallbacks pointerCallbacks = default;

        [Header("Events")]
        [SerializeField] private List<TweenPlayer> onDown = default;
        [SerializeField] private List<TweenPlayer> onUp = default;
        [SerializeField] private List<TweenPlayer> onClick = default;
        [SerializeField] private List<TweenPlayer> onEnter = default;
        [SerializeField] private List<TweenPlayer> onExit = default;

        private void Awake()
        {
            pointerCallbacks.OnDown += OnPointerCallbacksDown;
            pointerCallbacks.OnUp += OnPointerCallbacksUp;
            pointerCallbacks.OnClick += OnPointerCallbacksClick;
            pointerCallbacks.OnEnter += OnPointerCallbacksEnter;
            pointerCallbacks.OnExit += OnPointerCallbacksExit;
        }

        private void OnDestroy()
        {
            pointerCallbacks.OnDown -= OnPointerCallbacksDown;
            pointerCallbacks.OnUp -= OnPointerCallbacksUp;
            pointerCallbacks.OnClick -= OnPointerCallbacksClick;
            pointerCallbacks.OnEnter -= OnPointerCallbacksEnter;
            pointerCallbacks.OnExit -= OnPointerCallbacksExit;
        }

        private void OnPointerCallbacksDown( PointerCallbacks pointerCallbacks, PointerEventData pointerEventData )
        {
            Kill(onUp, onClick, onEnter, onExit);
            Play(onDown);
        }

        private void OnPointerCallbacksUp( PointerCallbacks pointerCallbacks, PointerEventData pointerEventData )
        {
            Kill(onDown, onClick, onEnter, onExit);
            Play(onUp);
        }

        private void OnPointerCallbacksClick( PointerCallbacks pointerCallbacks, PointerEventData pointerEventData )
        {
            Kill(onUp, onDown, onEnter, onExit);
            Play(onClick);
        }

        private void OnPointerCallbacksEnter(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            Kill(onUp, onDown, onClick, onExit);
            Play(onEnter);
        }


        private void OnPointerCallbacksExit(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            Kill(onUp, onDown, onClick, onEnter);
            Play(onExit);
        }


        private void Play(IReadOnlyList<TweenPlayer> tweenPlayers)
        {
            foreach (TweenPlayer tweenPlayer in tweenPlayers)
            {
                tweenPlayer.Play();
            }
        }

        private void Kill(params IReadOnlyList<TweenPlayer>[] tweenPlayers)
        {
            foreach (IReadOnlyList<TweenPlayer> tweenPlayerList in tweenPlayers)
            {
                foreach (TweenPlayer tweenPlayer in tweenPlayerList)
                {
                    tweenPlayer.Kill();
                }
            }
        }
    }
}