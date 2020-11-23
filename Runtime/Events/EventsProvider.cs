using Juce.Utils.Singletons;
using System;
using System.Collections.Generic;

namespace Juce.CoreUnity.Events
{
    public class EventsProvider : AutoStartMonoSingleton<EventsProvider>
    {
        private readonly Dictionary<Type, List<EventReference>> eventHandlers = new Dictionary<Type, List<EventReference>>();

        public EventReference Subscribe<T>(Action<T> onInvoked)
        {
            Type type = typeof(T);

            Action<object> action = (object obj) =>
            {
                T objGen = (T)obj;

                onInvoked?.Invoke(objGen);
            };

            EventReference newHandler = new EventReference(type, action);

            eventHandlers.TryGetValue(type, out List<EventReference> eventDataList);

            if (eventDataList == null)
            {
                eventDataList = new List<EventReference>();
                eventHandlers.Add(type, eventDataList);
            }

            eventDataList.Add(newHandler);

            return newHandler;
        }

        public void Unsubscribe(EventReference eventReference)
        {
            if (eventReference == null)
            {
                throw new ArgumentNullException($"Tried to unsubscribe {nameof(EventReference)} but it was null at {nameof(EventsProvider)}");
            }

            List<EventReference> eventDataList;
            eventHandlers.TryGetValue(eventReference.Type, out eventDataList);

            if (eventDataList == null)
            {
                return;
            }

            eventDataList.Remove(eventReference);
        }

        public void Invoke<T>(T obj)
        {
            Type type = typeof(T);

            eventHandlers.TryGetValue(type, out List<EventReference> eventDataList);

            if (eventDataList == null)
            {
                return;
            }

            for (int i = 0; i < eventDataList.Count; ++i)
            {
                eventDataList[i].Action?.Invoke(obj);
            }
        }
    }
}