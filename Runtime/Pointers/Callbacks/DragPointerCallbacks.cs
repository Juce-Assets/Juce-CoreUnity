using Juce.Core.Events.Generic;
using Juce.Core.Events.Consumer;
using UnityEngine;
using UnityEngine.EventSystems;
using Juce.CoreUnity.Pointers.Enums;

namespace Juce.CoreUnity.Pointers.Callbacks
{
    public class DragPointerCallbacks : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private readonly EventConsumer<DragPointerCallbacksEvents> eventConsumer = new EventConsumer<DragPointerCallbacksEvents>();

        private DragPointerCallbacksEvents state = DragPointerCallbacksEvents.End;

        public event GenericEvent<DragPointerCallbacks, PointerEventData> OnBegin;
        public event GenericEvent<DragPointerCallbacks, PointerEventData> OnDragging;
        public event GenericEvent<DragPointerCallbacks, PointerEventData> OnEnd;

        private void OnApplicationFocus(bool hasFocus)
        {
            TrySetState(DragPointerCallbacksEvents.End, new PointerEventData(EventSystem.current));
        }

        public void Consume(DragPointerCallbacksEvents ev)
        {
            eventConsumer.Consume(ev);
        }

        public void OnBeginDrag(PointerEventData pointerEventData)
        {
            TrySetState(DragPointerCallbacksEvents.Begin, pointerEventData);
        }

        public void OnDrag(PointerEventData pointerEventData)
        {
            TrySetState(DragPointerCallbacksEvents.Dragging, pointerEventData);
        }

        public void OnEndDrag(PointerEventData pointerEventData)
        {
            TrySetState(DragPointerCallbacksEvents.End, pointerEventData);
        }

        private void TrySetState(DragPointerCallbacksEvents state, PointerEventData pointerEventData)
        {
            switch (state)
            {
                case DragPointerCallbacksEvents.Begin:
                    {
                        if (this.state == DragPointerCallbacksEvents.End)
                        {
                            this.state = state;

                            bool alreadyConsumed = eventConsumer.Pop(DragPointerCallbacksEvents.Begin);

                            if (!alreadyConsumed)
                            {
                                OnBegin?.Invoke(this, pointerEventData);
                            }
                        }
                    }
                    break;

                case DragPointerCallbacksEvents.Dragging:
                    {
                        if (this.state == DragPointerCallbacksEvents.Begin || this.state == DragPointerCallbacksEvents.Dragging)
                        {
                            this.state = state;

                            bool alreadyConsumed = eventConsumer.Pop(DragPointerCallbacksEvents.Dragging);

                            if (!alreadyConsumed)
                            {
                                OnDragging?.Invoke(this, pointerEventData);
                            }
                        }
                    }
                    break;

                case DragPointerCallbacksEvents.End:
                    {
                        if (this.state == DragPointerCallbacksEvents.Dragging)
                        {
                            this.state = state;

                            bool alreadyConsumed = eventConsumer.Pop(DragPointerCallbacksEvents.End);

                            if (!alreadyConsumed)
                            {
                                OnEnd?.Invoke(this, pointerEventData);
                            }
                        }
                    }
                    break;
            }
        }
    }
}
