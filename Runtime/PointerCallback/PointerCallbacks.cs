using Juce.Core.Events.Generic;
using Juce.CoreUnity.Events.Consumer;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Juce.CoreUnity.PointerCallback
{
    public class PointerCallbacks : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private readonly EventConsumer<PointerCallbackEvents> eventConsumer = new EventConsumer<PointerCallbackEvents>();

        private PointerCallbackPressState pressState = PointerCallbackPressState.Up;
        private PointerCallbackPositionState positionState = PointerCallbackPositionState.Out;

        public event GenericEvent<PointerCallbacks, PointerEventData> OnEnter;
        public event GenericEvent<PointerCallbacks, PointerEventData> OnExit;
        public event GenericEvent<PointerCallbacks, PointerEventData> OnDown;
        public event GenericEvent<PointerCallbacks, PointerEventData> OnUp;
        public event GenericEvent<PointerCallbacks, PointerEventData> OnClick;

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                TrySetPressState(PointerCallbackPressState.Up, new PointerEventData(EventSystem.current));
                TrySetPositionState(PointerCallbackPositionState.Out, new PointerEventData(EventSystem.current));
            }
        }

        public void Consume(PointerCallbackEvents ev)
        {
            eventConsumer.Consume(ev);
        }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            TrySetPressState(PointerCallbackPressState.Down, pointerEventData);
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            TrySetPressState(PointerCallbackPressState.Up, pointerEventData);
            TrySetPositionState(PointerCallbackPositionState.Out, pointerEventData);
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            TrySetPositionState(PointerCallbackPositionState.In, pointerEventData);
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            TrySetPositionState(PointerCallbackPositionState.Out, pointerEventData);
            TrySetPressState(PointerCallbackPressState.Up, pointerEventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            bool alreadyConsumed = eventConsumer.Pop(PointerCallbackEvents.Click);

            if (!alreadyConsumed)
            {
                OnClick?.Invoke(this, eventData);
            }
        }

        private void TrySetPressState(PointerCallbackPressState pressState, PointerEventData pointerEventData)
        {
            switch (pressState)
            {
                case PointerCallbackPressState.Up:
                    {
                        if (this.pressState == PointerCallbackPressState.Down)
                        {
                            this.pressState = pressState;

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
                        if (this.pressState == PointerCallbackPressState.Up)
                        {
                            this.pressState = pressState;

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
                        if (this.positionState == PointerCallbackPositionState.Out)
                        {
                            this.positionState = positionState;

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
                        if (this.positionState == PointerCallbackPositionState.In)
                        {
                            this.positionState = positionState;

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
