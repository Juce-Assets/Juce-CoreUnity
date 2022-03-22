using Juce.CoreUnity.PointerCallback;
using Juce.CoreUnity.Ui.SelectableCallback;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Juce.CoreUnity.Ui.Audio
{
    public class PointerAndSelectableCallbacksAudioPlayer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PointerCallbacks pointerCallbacks = default;
        [SerializeField] private SelectableCallbacks selectableCallbacks = default;
        [SerializeField] private AudioSource audioSource = default;

        [Header("Clips")]
        [SerializeField] private AudioClip onUpClip = default;
        [SerializeField] private AudioClip onDownClip = default;
        [SerializeField] private AudioClip onEnterSelectClip = default;
        [SerializeField] private AudioClip onExitDeselectClip = default;
        [SerializeField] private AudioClip onClickSubmitClip = default;

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
            if (onUpClip == null)
            {
                return;
            }

            audioSource.PlayOneShot(onUpClip);
        }

        private void OnPointerCallbacksDown(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            if (onDownClip == null)
            {
                return;
            }

            audioSource.PlayOneShot(onDownClip);
        }

        private void OnPointerCallbacksEnter(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            if (onEnterSelectClip == null)
            {
                return;
            }

            audioSource.PlayOneShot(onEnterSelectClip);
        }

        private void OnPointerCallbacksExit(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            if (onExitDeselectClip == null)
            {
                return;
            }

            audioSource.PlayOneShot(onExitDeselectClip);
        }

        private void OnPointerCallbacksClick(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            if (onClickSubmitClip == null)
            {
                return;
            }

            audioSource.PlayOneShot(onClickSubmitClip);
        }

        private void OnSelectableCallbacksSelected(SelectableCallbacks selectableCallbacks, BaseEventData baseEventData)
        {
            if (onEnterSelectClip == null)
            {
                return;
            }

            audioSource.PlayOneShot(onEnterSelectClip);
        }

        private void OnSelectableCallbacksDeselected(SelectableCallbacks selectableCallbacks, BaseEventData baseEventData)
        {
            if (onExitDeselectClip == null)
            {
                return;
            }

            audioSource.PlayOneShot(onExitDeselectClip);
        }

        private void OnSelectableCallbacksSubmited(SelectableCallbacks selectableCallbacks, BaseEventData baseEventData)
        {
            if (onClickSubmitClip == null)
            {
                return;
            }

            audioSource.PlayOneShot(onClickSubmitClip);
        }
    }
}
