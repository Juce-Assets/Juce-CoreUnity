using System;
using System.Collections.Generic;

namespace Juce.CoreUnity.Events
{
    public class EventConsumer<T> where T : Enum
    {
        private readonly Dictionary<T, int> consumedEvents = new Dictionary<T, int>();

        public void Consume(T ev)
        {
            bool alreadyAdded = consumedEvents.ContainsKey(ev);

            if (!alreadyAdded)
            {
                consumedEvents.Add(ev, 1);
            }
            else
            {
                consumedEvents[ev] += 1;
            }
        }

        public bool Pop(T ev)
        {
            bool contains = consumedEvents.TryGetValue(ev, out int count);

            if (!contains)
            {
                return false;
            }

            if (count == 0)
            {
                return false;
            }

            consumedEvents[ev] -= 1;

            return true;
        }
    }
}
