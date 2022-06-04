#if JUCE_CINEMACHINE_EXTENSIONS

using UnityEngine;
using Cinemachine;
using Juce.Core.Time;
using Juce.CoreUnity.Time;
using System;
using static Cinemachine.CinemachineImpulseManager;

namespace Juce.CoreUnity.Cinemachine.Impulse
{
    public sealed class CinemachineContinuousImpulse : MonoBehaviour
    {
        [CinemachineImpulseDefinitionProperty]
        [SerializeField] private CinemachineImpulseDefinition impulseDefinition = new CinemachineImpulseDefinition();

        private readonly ITimer timer = new ScaledUnityTimer();

        private ImpulseEvent impulseEvent;

        public bool Active { get; set; }

        private void Update()
        {
            UpdateImpulse();
        }

        private void UpdateImpulse()
        {
            if (!Active)
            {
                TryCancelPlayingEvent();
                return;
            }

            float eventLength = impulseDefinition.m_TimeEnvelope.m_AttackTime + impulseDefinition.m_TimeEnvelope.m_SustainTime;

            bool timePassed = timer.HasReached(TimeSpan.FromSeconds(eventLength)) || !timer.StartedAndNotPaused;

            if (!timePassed)
            {
                return;
            }

            timer.Restart();

            impulseEvent = impulseDefinition.CreateAndReturnEvent(transform.position, Vector3.one);

            bool thereIsAnAlreadyPlayingEvent = impulseEvent != null;

            if (!thereIsAnAlreadyPlayingEvent)
            {
                return;
            }

            impulseEvent.m_Envelope.m_SustainTime = impulseEvent.m_Envelope.m_SustainTime + impulseEvent.m_Envelope.m_AttackTime;
            impulseEvent.m_Envelope.m_AttackTime = 0.0f;
        }

        private void TryCancelPlayingEvent()
        {
            if(impulseEvent == null)
            {
                return;
            }

            impulseEvent.Cancel(time: 0, forceNoDecay: false);

            impulseEvent = null;

            timer.Pause();
        }
    }
}

#endif