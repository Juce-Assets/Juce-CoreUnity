using Juce.Core.Events.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Juce.CoreUnity.Ui.SelectableCallback
{
    public class SelectableCallbacks : Selectable, ISubmitHandler
    {
        public bool Selected { get; private set; }

        public event GenericEvent<SelectableCallbacks, BaseEventData> OnSelected;
        public event GenericEvent<SelectableCallbacks, BaseEventData> OnDeselected;
        public event GenericEvent<SelectableCallbacks, BaseEventData> OnSubmited;

        public void SetAsSelected()
        {
            Select();

            OnSelect(new BaseEventData(EventSystem.current));
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            bool isInteractable = IsInteractable();

            if (!isInteractable)
            {
                return;
            }

            Selected = true;

            OnSelected?.Invoke(this, eventData);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            Selected = false;

            OnDeselected?.Invoke(this, eventData);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if (!Selected)
            {
                return;
            }

            bool isInteractable = IsInteractable();

            if(!isInteractable)
            {
                return;
            }

            OnSubmited?.Invoke(this, eventData);
        }
    }
}
