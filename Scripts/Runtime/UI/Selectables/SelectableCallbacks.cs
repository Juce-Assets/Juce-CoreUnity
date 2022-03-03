﻿using Juce.Core.Events.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Juce.CoreUnity.Ui
{
    public class SelectableCallbacks : Selectable
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

            if (Selected)
            {
                return;
            }

            Selected = true;

            OnSelected?.Invoke(this, eventData);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            if (!Selected)
            {
                return;
            }

            Selected = false;

            OnDeselected?.Invoke(this, eventData);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if (!Selected)
            {
                return;
            }

            OnSubmited?.Invoke(this, eventData);
        }
    }
}