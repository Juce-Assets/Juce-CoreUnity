using Juce.CoreUnity.Pointers.Callbacks;
using Juce.CoreUnity.Ui.SelectableCallback;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Juce.CoreUnity.Audio.PointerAndSelectableCallbacks
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

        private void Play(AudioClip audioClip)
        {
            if(audioClip == null)
            {
                return;
            }

            if(!audioSource.isActiveAndEnabled)
            {
                return;
            }

            audioSource.PlayOneShot(audioClip);
        }

        private void OnPointerCallbacksUp(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            Play(onUpClip);
        }

        private void OnPointerCallbacksDown(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            Play(onDownClip);
        }

        private void OnPointerCallbacksEnter(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            Play(onEnterSelectClip);
        }

        private void OnPointerCallbacksExit(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            Play(onExitDeselectClip);
        }

        private void OnPointerCallbacksClick(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            Play(onClickSubmitClip);
        }

        private void OnSelectableCallbacksSelected(SelectableCallbacks selectableCallbacks, BaseEventData baseEventData)
        {
            Play(onEnterSelectClip);
        }

        private void OnSelectableCallbacksDeselected(SelectableCallbacks selectableCallbacks, BaseEventData baseEventData)
        {
            Play(onExitDeselectClip);
        }

        private void OnSelectableCallbacksSubmited(SelectableCallbacks selectableCallbacks, BaseEventData baseEventData)
        {
            Play(onClickSubmitClip);
        }
    }
}
