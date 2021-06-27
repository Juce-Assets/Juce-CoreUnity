using Juce.Core.Tickable;
using Juce.CoreUnity.Service;
using System.Collections.Generic;

namespace Juce.CoreUnity.Services
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
            if (tickable == null)
            {
                throw new System.ArgumentNullException($"Tried to add {nameof(ITickable)} but it was null at {nameof(TickablesService)}");
            }

            bool contains = tickables.Contains(tickable);

            if (contains)
            {
                throw new System.Exception($"Tried to add {nameof(ITickable)} but it was already at {nameof(TickablesService)}");
            }

            tickables.Add(tickable);
        }

        public void RemoveTickable(ITickable tickable)
        {
            if (tickable == null)
            {
                throw new System.ArgumentNullException($"Tried to remove {nameof(ITickable)} but it was null at {nameof(TickablesService)}");
            }

            bool contained = tickables.Contains(tickable);

            if (!contained)
            {
                return;
            }

            bool alreadyToRemove = tickablesToRemove.Contains(tickable);

            if (alreadyToRemove)
            {
                return;
            }

            tickablesToRemove.Add(tickable);
        }

        private void ActuallyRemoveTickables()
        {
            for (int i = 0; i < tickablesToRemove.Count; ++i)
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
            for (int i = 0; i < tickables.Count; ++i)
            {
                tickables[i].Tick();
            }
        }
    }
}