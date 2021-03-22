using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Juce.CoreUnity.Pointer
{
    public class PointerHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {
        public event Action<PointerEventData> OnClick;

        public event Action<PointerEventData> OnDown;

        public event Action<PointerEventData> OnUp;

        public event Action<PointerEventData> OnEnter;

        public event Action<PointerEventData> OnExit;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDown?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnUp?.Invoke(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnEnter?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExit?.Invoke(eventData);
        }
    }
}