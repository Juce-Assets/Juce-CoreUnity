using Juce.CoreUnity.Pointers.Callbacks;
using Juce.CoreUnity.Ui.SelectableCallback;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Juce.CoreUnity.Ui.Others
{
    public class PointerAndSelectableSubmitCallbacks : MonoBehaviour
    {
        [SerializeField] private PointerCallbacks pointerCallbacks = default;
        [SerializeField] private SelectableCallbacks selectableCallbacks = default;

        public event Action OnSubmit;

        private void Awake()
        {
            pointerCallbacks.OnClick += OnPointerCallbacksClick;
            selectableCallbacks.OnSubmited += OnSelectableCallbacksSubmited;
        }

        private void OnPointerCallbacksClick(PointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            OnSubmit?.Invoke();
        }

        private void OnSelectableCallbacksSubmited(SelectableCallbacks selectableCallbacks, BaseEventData baseEventData)
        {
            OnSubmit?.Invoke();
        }
    }
}
