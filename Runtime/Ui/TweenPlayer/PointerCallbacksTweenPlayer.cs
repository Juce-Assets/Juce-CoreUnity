using Juce.Core.Extensions;
using Juce.CoreUnity.PointerCallback;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Juce.CoreUnity.Ui.TweenPlayer
{
    public class PointerCallbacksTweenPlayer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PointerCallbacks pointerCallbacks = default;

        [Header("Animations")]
        [SerializeField] private TaskAnimationMonoBehaviour onUpAnimation = default;
        [SerializeField] private TaskAnimationMonoBehaviour onDownAnimation = default;
        [SerializeField] private TaskAnimationMonoBehaviour onEnterAnimation = default;
        [SerializeField] private TaskAnimationMonoBehaviour onExitAnimation = default;
        [SerializeField] private TaskAnimationMonoBehaviour onClickAnimation = default;

        private void Awake()
        {
            pointerCallbacks.OnUp += OnPointerCallbacksUp;
            pointerCallbacks.OnDown += OnPointerCallbacksDown;
            pointerCallbacks.OnEnter += OnPointerCallbacksEnter;
            pointerCallbacks.OnExit += OnPointerCallbacksExit;
            pointerCallbacks.OnClick += OnPointerCallbacksClick;
        }

        private void OnDestroy()
        {
            pointerCallbacks.OnUp -= OnPointerCallbacksUp;
            pointerCallbacks.OnDown -= OnPointerCallbacksDown;
            pointerCallbacks.OnEnter -= OnPointerCallbacksEnter;
            pointerCallbacks.OnExit -= OnPointerCallbacksExit;
            pointerCallbacks.OnClick -= OnPointerCallbacksClick;
        }

        private void OnPointerCallbacksUp(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            if(onUpAnimation == null)
            {
                return;
            }

            onUpAnimation.Execute(instantly: false, default).RunAsync();
        }

        private void OnPointerCallbacksDown(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            if (onDownAnimation == null)
            {
                return;
            }

            onDownAnimation.Execute(instantly: false, default).RunAsync();
        }

        private void OnPointerCallbacksEnter(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            if (onEnterAnimation == null)
            {
                return;
            }

            onEnterAnimation.Execute(instantly: false, default).RunAsync();
        }

        private void OnPointerCallbacksExit(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            if (onExitAnimation == null)
            {
                return;
            }

            onExitAnimation.Execute(instantly: false, default).RunAsync();
        }

        private void OnPointerCallbacksClick(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            if (onClickAnimation == null)
            {
                return;
            }

            onClickAnimation.Execute(instantly: false, default).RunAsync();
        }
    }
}
