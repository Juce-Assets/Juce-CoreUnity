using Juce.CoreUnity.PointerCallback;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Juce.CoreUnity.Ui
{
    public class PointerCallbacksSelectable : Selectable
    {
        [SerializeField] private PointerCallbacks pointerCallbacks = default;

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            pointerCallbacks.OnPointerEnter(new PointerEventData(EventSystem.current));
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            pointerCallbacks.OnPointerExit(new PointerEventData(EventSystem.current));
        }

        public void OnSubmit(BaseEventData eventData)
        {
            pointerCallbacks.OnPointerClick(new PointerEventData(EventSystem.current));
        }
    }
}
