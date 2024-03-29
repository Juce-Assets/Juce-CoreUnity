﻿using Juce.Core.Events.Generic;
using Juce.CoreUnity.Pointers.Configuration;
using Juce.CoreUnity.Pointers.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Juce.CoreUnity.Pointers.Callbacks
{
    public class LongPressPointerCallbacks : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private PointerCallbacks pointerCallbacks = default;

        [Header("Configuration")]
        [SerializeField] private bool consumeOnUpWhenLongPressDown = default;
        [SerializeField] private LongPressPointerCallbacksConfiguration pointerCallbacksConfiguration = default;

        private PointerEventData lastOnDownPointerEventData;

        private float nextLongPressActivationTime = float.PositiveInfinity;

        private bool canLongPressUp;

        public PointerCallbacks PointerCallbacks => pointerCallbacks;

        public event GenericEvent<LongPressPointerCallbacks, PointerEventData> OnLongPressDown;
        public event GenericEvent<LongPressPointerCallbacks, PointerEventData> OnLongPressUp;

        private void Awake()
        {
            RegisterPointerCallbacks();
        }

        private void OnDestroy()
        {
            UnregisterPointerCallbacks();
        }

        private void Update()
        {
            TryActivateLongPressDown();
        }

        private void RegisterPointerCallbacks()
        {
            if (pointerCallbacks == null)
            {
                UnityEngine.Debug.LogError($"Tried to register from {nameof(PointerCallbacks)}, but it was null, " +
                    $"at {nameof(LongPressPointerCallbacks)}", this);
                return;
            }

            pointerCallbacks.OnDown += OnDown;
            pointerCallbacks.OnUp += OnUp;
        }

        private void UnregisterPointerCallbacks()
        {
            if (pointerCallbacks == null)
            {
                UnityEngine.Debug.LogError($"Tried to unregister from {nameof(PointerCallbacks)}, but it was null, " +
                    $"at {nameof(LongPressPointerCallbacks)}", this);
                return;
            }

            pointerCallbacks.OnDown -= OnDown;
            pointerCallbacks.OnUp -= OnUp;
        }

        private void OnDown(PointerCallbacks owner, PointerEventData data)
        {
            lastOnDownPointerEventData = data;

            nextLongPressActivationTime = UnityEngine.Time.unscaledTime + pointerCallbacksConfiguration.LongPressActivationDelay;
        }

        private void OnUp(PointerCallbacks owner, PointerEventData data)
        {
            nextLongPressActivationTime = float.PositiveInfinity;

            TryActivateLongPressUp();
        }

        private void TryActivateLongPressDown()
        {
            if (UnityEngine.Time.unscaledTime < nextLongPressActivationTime)
            {
                return;
            }

            nextLongPressActivationTime = float.PositiveInfinity;

            canLongPressUp = true;

            OnLongPressDown?.Invoke(this, lastOnDownPointerEventData);

            if (consumeOnUpWhenLongPressDown)
            {
                PointerCallbacks.Consume(PointerCallbackEvents.Up);
            }
        }

        private void TryActivateLongPressUp()
        {
            if (!canLongPressUp)
            {
                return;
            }

            canLongPressUp = false;

            OnLongPressUp?.Invoke(this, lastOnDownPointerEventData);
        }
    }
}
