using Juce.Core.Tick.Tickable;
using Juce.Core.Tick.Tickables;
using Juce.CoreUnity.Tick.Enums;
using UnityEngine;

namespace Juce.CoreUnity.Tick.Services
{
    public sealed class TickablesService : MonoBehaviour, ITickablesService
    {
        readonly TickablesContainerTickable preUpdateTickables = new TickablesContainerTickable();
        readonly TickablesContainerTickable updateTickables = new TickablesContainerTickable();
        readonly TickablesContainerTickable lateUpdateTickables = new TickablesContainerTickable();
        readonly TickablesContainerTickable fixedUpdateTickables = new TickablesContainerTickable();

        void Update()
        {
            preUpdateTickables.Tick();
            updateTickables.Tick();
        }

        void LateUpdate()
        {
            lateUpdateTickables.Tick();
        }

        void FixedUpdate()
        {
            fixedUpdateTickables.Tick();
        }

        public void Add(ITickable tickable, TickType tickType)
        {
            switch (tickType)
            {
                case TickType.PreUpdate:
                {
                    preUpdateTickables.Add(tickable);
                    break;
                }

                default:
                case TickType.Update:
                {
                    updateTickables.Add(tickable);
                    break;
                }

                case TickType.LateUpdate:
                {
                    lateUpdateTickables.Add(tickable);
                    break;
                }

                case TickType.FixedUpdate:
                {
                    fixedUpdateTickables.Add(tickable);
                    break;
                }
            }
        }

        public void Remove(ITickable tickable, TickType tickType)
        {
            switch (tickType)
            {
                case TickType.PreUpdate:
                {
                    preUpdateTickables.Remove(tickable);
                    break;
                }

                default:
                case TickType.Update:
                {
                    updateTickables.Remove(tickable);
                    break;
                }

                case TickType.LateUpdate:
                {
                    lateUpdateTickables.Remove(tickable);
                    break;
                }

                case TickType.FixedUpdate:
                {
                    fixedUpdateTickables.Remove(tickable);
                    break;
                }
            }
        }

        public void RemoveNow(ITickable tickable, TickType tickType)
        {
            switch (tickType)
            {
                case TickType.PreUpdate:
                {
                    preUpdateTickables.Remove(tickable);
                    preUpdateTickables.ActuallyRemoveTickables();
                    break;
                }

                default:
                case TickType.Update:
                {
                    updateTickables.Remove(tickable);
                    updateTickables.ActuallyRemoveTickables();
                    break;
                }

                case TickType.LateUpdate:
                {
                    lateUpdateTickables.Remove(tickable);
                    lateUpdateTickables.ActuallyRemoveTickables();
                    break;
                }

                case TickType.FixedUpdate:
                {
                    fixedUpdateTickables.Remove(tickable);
                    fixedUpdateTickables.ActuallyRemoveTickables();
                    break;
                }
            }
        }
    }
}
