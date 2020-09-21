using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Juce.Core.Contracts;

namespace Juce.Core.Particles
{
    public class ParticleSystemHelper : MonoBehaviour
    {
        public event Action OnParticleSystemFinished;

        private ParticleSystem particle;

        private bool playing;

        private void Awake()
        {
            particle = GetComponent<ParticleSystem>();

            Contract.IsNotNull(particle, $"{nameof(ParticleSystemHelper)} needs a particle system component to function");

            ParticleSystem.MainModule mainModule = particle.main;
            mainModule.stopAction = ParticleSystemStopAction.Callback;

            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            mainModule.playOnAwake = false;

            playing = false;
        }

        private void OnParticleSystemStopped()
        {
            if(!playing)
            {
                return;
            }

            playing = false;

            OnParticleSystemFinished?.Invoke();
        }

        public void Play()
        {
            if (particle == null)
            {
                return;
            }

            particle.Play(true);

            playing = true;
        }

        public void Stop(ParticleSystemStopBehavior stopBehavior)
        {
            if (particle == null)
            {
                return;
            }

            particle.Stop(true, stopBehavior);
        }

        public bool IsPlaying()
        {
            if(particle == null)
            {
                return false;
            }

            return particle.IsAlive(true);
        }

        public async Task AwaitForFinish(CancellationToken cancellationToken)
        {
            while(IsPlaying() && !cancellationToken.IsCancellationRequested)
            {
                await Task.Yield();
            }
        }
    }
}
