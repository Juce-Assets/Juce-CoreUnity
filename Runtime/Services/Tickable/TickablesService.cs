using System;
using System.Collections.Generic;
using Juce.Utils.Contracts;
using Juce.Core.Service;
using Juce.Core.Tickable;

namespace Juce.Core.Services
{
    public class TickablesService : IUpdatableService
    {
        private readonly List<ITickable> tickables = new List<ITickable>();
        private readonly List<ITickable> tickablesToRemove = new List<ITickable>();

        public void Init()
        {

        }

        public void Update()
        {
            ActuallyRemoveTickables();

            TickTickables();
        }

        public void CleanUp()
        {
            RemoveAllTickables();
        }

        public void AddTickable(ITickable tickable)
        {
            bool contains = tickables.Contains(tickable);

            Contract.IsFalse(contains, $"Trying to add {nameof(ITickable)} but it was already on {nameof(TickablesService)}");

            tickables.Add(tickable);
        }

        public void RemoveTickable(ITickable tickable)
        {
            bool contained = tickables.Contains(tickable);

            Contract.IsTrue(contained, $"Tried to remove {nameof(ITickable)}, but it was not added " +
                $"on the first place on {nameof(TickablesService)}");

            bool alreadyToRemove = tickablesToRemove.Contains(tickable);

            Contract.IsFalse(alreadyToRemove, $"Tried to remove {nameof(ITickable)}, but it was not added " +
                $"on the first place on {nameof(TickablesService)}");

            tickablesToRemove.Add(tickable);
        }

        private void ActuallyRemoveTickables()
        {
            for(int i = 0; i < tickablesToRemove.Count; ++i)
            {
                tickables.Remove(tickablesToRemove[i]);
            }

            tickablesToRemove.Clear();
        }

        private void RemoveAllTickables()
        {
            tickablesToRemove.AddRange(tickables);

            ActuallyRemoveTickables();
        }

        private void TickTickables()
        {
            for(int i = 0; i < tickables.Count; ++i)
            {
                tickables[i].Tick();
            }
        }
    }
}
