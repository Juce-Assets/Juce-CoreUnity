using Juce.CoreUnity.PointerCallback;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Juce.CoreUnity.Ui
{
    public class PointerCallbacksSelectable : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler
    {
        [SerializeField] private PointerCallbacks pointerCallbacks = default;

        public void OnSelect(BaseEventData eventData)
        {
            pointerCallbacks.OnPointerEnter(new PointerEventData(EventSystem.current));
        }

        public void OnDeselect(BaseEventData eventData)
        {
            pointerCallbacks.OnPointerExit(new PointerEventData(EventSystem.current));
        }

        public void OnSubmit(BaseEventData eventData)
        {
            pointerCallbacks.OnPointerClick(new PointerEventData(EventSystem.current));
        }
    }
}
