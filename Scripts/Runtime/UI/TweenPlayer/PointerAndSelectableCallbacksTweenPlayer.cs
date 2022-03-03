using Juce.CoreUnity.PointerCallback;
using Juce.CoreUnity.Ui.SelectableCallback;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Juce.CoreUnity.Ui.TweenPlayer
{
    public class PointerAndSelectableCallbacksTweenPlayer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PointerCallbacks pointerCallbacks = default;
        [SerializeField] private SelectableCallbacks selectableCallbacks = default;

        [Header("PointerCallbacks Animations")]
        [SerializeField] private TaskAnimationMonoBehaviour onUpAnimation = default;
        [SerializeField] private TaskAnimationMonoBehaviour onDownAnimation = default;
        [SerializeField] private TaskAnimationMonoBehaviour onEnterSelectAnimation = default;
        [SerializeField] private TaskAnimationMonoBehaviour onExitDeselectAnimation = default;
        [SerializeField] private TaskAnimationMonoBehaviour onClickSubmitAnimation = default;

        [Header("Configuration")]
        [SerializeField] private bool executeSelectedAfterSubmit = true;

        private void Awake()
        {
            pointerCallbacks.OnUp += OnPointerCallbacksUp;
            pointerCallbacks.OnDown += OnPointerCallbacksDown;
            pointerCallbacks.OnEnter += OnPointerCallbacksEnter;
            pointerCallbacks.OnExit += OnPointerCallbacksExit;
            pointerCallbacks.OnClick += OnPointerCallbacksClick;

            selectableCallbacks.OnSelected += OnSelectableCallbacksSelected;
            selectableCallbacks.OnDeselected += OnSelectableCallbacksDeselected;
            selectableCallbacks.OnSubmited += OnSelectableCallbacksSubmited;
        }

        private void OnDestroy()
        {
            pointerCallbacks.OnUp -= OnPointerCallbacksUp;
            pointerCallbacks.OnDown -= OnPointerCallbacksDown;
            pointerCallbacks.OnEnter -= OnPointerCallbacksEnter;
            pointerCallbacks.OnExit -= OnPointerCallbacksExit;
            pointerCallbacks.OnClick -= OnPointerCallbacksClick;

            selectableCallbacks.OnSelected -= OnSelectableCallbacksSelected;
            selectableCallbacks.OnDeselected -= OnSelectableCallbacksDeselected;
            selectableCallbacks.OnSubmited -= OnSelectableCallbacksSubmited;
        }

        private void OnPointerCallbacksUp(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            if (onUpAnimation == null)
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
            if (onEnterSelectAnimation == null)
            {
                return;
            }

            if(selectableCallbacks.Selected)
            {
                return;
            }

            onEnterSelectAnimation.Execute(instantly: false, default).RunAsync();
        }

        private void OnPointerCallbacksExit(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            if (onExitDeselectAnimation == null)
            {
                return;
            }

            if (selectableCallbacks.Selected)
            {
                return;
            }

            onExitDeselectAnimation.Execute(instantly: false, default).RunAsync();
        }

        private void OnPointerCallbacksClick(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            if (onClickSubmitAnimation == null)
            {
                return;
            }

            onClickSubmitAnimation.Execute(instantly: false, default).RunAsync();
        }

        private void OnSelectableCallbacksSelected(SelectableCallbacks selectableCallbacks, BaseEventData baseEventData)
        {
            if (onEnterSelectAnimation == null)
            {
                return;
            }

            if (pointerCallbacks.PositionState == PointerCallbackPositionState.In)
            {
                return;
            }

            onEnterSelectAnimation.Execute(instantly: false, default).RunAsync();
        }

        private void OnSelectableCallbacksDeselected(SelectableCallbacks selectableCallbacks, BaseEventData baseEventData)
        {
            if (onExitDeselectAnimation == null)
            {
                return;
            }

            if (pointerCallbacks.PositionState == PointerCallbackPositionState.In)
            {
                return;
            }

            onExitDeselectAnimation.Execute(instantly: false, default).RunAsync();
        }

        private void OnSelectableCallbacksSubmited(SelectableCallbacks selectableCallbacks, BaseEventData baseEventData)
        {
            if (onClickSubmitAnimation == null)
            {
                return;
            }

            ExecuteSubmit(CancellationToken.None).RunAsync();
        }

        private async Task ExecuteSubmit(CancellationToken cancellationToken)
        {
            await onClickSubmitAnimation.Execute(instantly: false, cancellationToken);

            if (!selectableCallbacks.Selected)
            {
                return;
            }

            if (!executeSelectedAfterSubmit)
            {
                return;
            }

            await onEnterSelectAnimation.Execute(instantly: false, cancellationToken);
        }
    }
}
