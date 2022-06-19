using Juce.Core.Events.Consumer;
using Juce.Core.Events.Generic;
using Juce.CoreUnity.Pointers.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Juce.CoreUnity.Ui.Buttons
{
    public class ButtonCallbacks : Selectable, ISubmitHandler, IPointerClickHandler
    {
        [SerializeField] private bool triggerPointerUpOnPointerExit = true;

        private readonly EventConsumer<PointerCallbackEvents> eventConsumer = new EventConsumer<PointerCallbackEvents>();

        public PointerCallbackPressState PressState { get; private set; } = PointerCallbackPressState.Up;
        public PointerCallbackPositionState PositionState { get; private set; } = PointerCallbackPositionState.Out;

        public bool Selected { get; private set; }

        public event GenericEvent<ButtonCallbacks, BaseEventData> OnSubmited;

        public event GenericEvent<ButtonCallbacks, BaseEventData> OnSelected;
        public event GenericEvent<ButtonCallbacks, BaseEventData> OnDeselected;

        public event GenericEvent<ButtonCallbacks, PointerEventData> OnEnter;
        public event GenericEvent<ButtonCallbacks, PointerEventData> OnExit;
        public event GenericEvent<ButtonCallbacks, PointerEventData> OnDown;
        public event GenericEvent<ButtonCallbacks, PointerEventData> OnUp;

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                TrySetPressState(PointerCallbackPressState.Up, new PointerEventData(EventSystem.current));
                TrySetPositionState(PointerCallbackPositionState.Out, new PointerEventData(EventSystem.current));
            }
        }

        public void SetAsSelected()
        {
            Select();

            OnSelect(new BaseEventData(EventSystem.current));
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

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

            if (!isInteractable)
            {
                return;
            }

            OnSubmited?.Invoke(this, eventData);
        }

        public void Consume(PointerCallbackEvents ev)
        {
            eventConsumer.Consume(ev);
        }

        public override void OnPointerDown(PointerEventData pointerEventData)
        {
            base.OnPointerDown(pointerEventData);

            bool isInteractable = IsInteractable();

            if (!isInteractable)
            {
                return;
            }

            TrySetPressState(PointerCallbackPressState.Down, pointerEventData);
        }

        public override void OnPointerUp(PointerEventData pointerEventData)
        {
            base.OnPointerUp(pointerEventData);

            TrySetPressState(PointerCallbackPressState.Up, pointerEventData);
            TrySetPositionState(PointerCallbackPositionState.Out, pointerEventData);
        }

        public override void OnPointerEnter(PointerEventData pointerEventData)
        {
            base.OnPointerEnter(pointerEventData);

            bool isInteractable = IsInteractable();

            if (!isInteractable)
            {
                return;
            }

            TrySetPositionState(PointerCallbackPositionState.In, pointerEventData);
        }

        public override void OnPointerExit(PointerEventData pointerEventData)
        {
            base.OnPointerExit(pointerEventData);

            TrySetPositionState(PointerCallbackPositionState.Out, pointerEventData);

            if (triggerPointerUpOnPointerExit)
            {
                TrySetPressState(PointerCallbackPressState.Up, pointerEventData);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            bool isInteractable = IsInteractable();

            if (!isInteractable)
            {
                return;
            }

            bool alreadyConsumed = eventConsumer.Pop(PointerCallbackEvents.Click);

            if (!alreadyConsumed)
            {
                OnSubmited?.Invoke(this, eventData);
            }
        }

        private void TrySetPressState(PointerCallbackPressState pressState, PointerEventData pointerEventData)
        {
            switch (pressState)
            {
                case PointerCallbackPressState.Up:
                    {
                        if (PressState == PointerCallbackPressState.Down)
                        {
                            PressState = pressState;

                            bool alreadyConsumed = eventConsumer.Pop(PointerCallbackEvents.Up);

                            if (!alreadyConsumed)
                            {
                                OnUp?.Invoke(this, pointerEventData);
                            }
                        }
                    }
                    break;

                case PointerCallbackPressState.Down:
                    {
                        if (PressState == PointerCallbackPressState.Up)
                        {
                            PressState = pressState;

                            bool alreadyConsumed = eventConsumer.Pop(PointerCallbackEvents.Down);

                            if (!alreadyConsumed)
                            {
                                OnDown?.Invoke(this, pointerEventData);
                            }
                        }
                    }
                    break;
            }
        }

        private void TrySetPositionState(PointerCallbackPositionState positionState, PointerEventData pointerEventData)
        {
            switch (positionState)
            {
                case PointerCallbackPositionState.In:
                    {
                        if (PositionState == PointerCallbackPositionState.Out)
                        {
                            PositionState = positionState;

                            bool alreadyConsumed = eventConsumer.Pop(PointerCallbackEvents.Enter);

                            if (!alreadyConsumed)
                            {
                                OnEnter?.Invoke(this, pointerEventData);
                            }
                        }
                    }
                    break;

                case PointerCallbackPositionState.Out:
                    {
                        if (PositionState == PointerCallbackPositionState.In)
                        {
                            PositionState = positionState;

                            bool alreadyConsumed = eventConsumer.Pop(PointerCallbackEvents.Exit);

                            if (!alreadyConsumed)
                            {
                                OnExit?.Invoke(this, pointerEventData);
                            }
                        }
                    }
                    break;
            }
        }
    }
}
