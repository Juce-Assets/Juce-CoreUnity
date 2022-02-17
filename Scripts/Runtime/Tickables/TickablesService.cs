using Juce.Core.Tickable;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.Tickables
{
    public class TickablesService : MonoBehaviour, ITickablesService
    {
        private readonly List<ITickable> tickables = new List<ITickable>();
        private readonly List<ITickable> tickablesToAdd = new List<ITickable>();
        private readonly List<ITickable> tickablesToRemove = new List<ITickable>();

        public void Update()
        {
            ActuallyRemoveTickables();

            TickTickables();

            ActuallyAddTickables();
        }

        public void OnDestroy()
        {
            Clear();
        }

        public void Add(ITickable tickable)
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

            bool alreadyToAdd = tickablesToAdd.Contains(tickable);

            if(alreadyToAdd)
            {
                return;
            }

            tickables.Add(tickable);
        }

        public void Remove(ITickable tickable)
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

        private void Clear()
        {
            tickablesToRemove.AddRange(tickables);

            ActuallyRemoveTickables();
        }

        private void ActuallyAddTickables()
        {
            foreach(ITickable tickable in tickablesToAdd)
            {
                tickables.Add(tickable);
            }

            tickablesToAdd.Clear();
        }

        private void ActuallyRemoveTickables()
        {
            foreach (ITickable tickable in tickablesToRemove)
            {
                tickables.Remove(tickable);
            }

            tickablesToRemove.Clear();
        }

        private void TickTickables()
        {
            foreach (ITickable tickable in tickables)
            {
                tickable.Tick();
            }
        }
    }
}