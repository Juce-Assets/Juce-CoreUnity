using System;
using System.Collections.Generic;
using Juce.Core.Contracts;
using Juce.Core.Singletons;

namespace Juce.Core.Events
{
    public class EventsProvider : MonoSingleton<EventsProvider>
    {
        private readonly Dictionary<Type, List<EventReference>> eventHandlers = new Dictionary<Type, List<EventReference>>();

        EventsProvider()
        {
            InitInstance(this);
        }

        public EventReference Subscribe<T>(Action<T> onInvoked) 
        {
            Type type = typeof(T);

            Action<object> action = (object obj) =>
            {
                T objGen = (T)obj;

                onInvoked?.Invoke(objGen);
            };

            EventReference newHandler = new EventReference(type, action);

            List<EventReference> eventDataList;
            eventHandlers.TryGetValue(type, out eventDataList);

            if(eventDataList == null)
            {
                eventDataList = new List<EventReference>();
                eventHandlers.Add(type, eventDataList);
            }

            eventDataList.Add(newHandler);

            return newHandler;
        }

        public void Unsubscribe(EventReference evHandler)
        {
            Contract.IsNotNull(evHandler);

            List<EventReference> eventDataList;
            eventHandlers.TryGetValue(evHandler.Type, out eventDataList);

            if (eventDataList == null)
            {
                return;
            }

            eventDataList.Remove(evHandler);
        }

        public void Invoke<T>(T obj) 
        {
            Type type = typeof(T);

            List<EventReference> eventDataList;
            eventHandlers.TryGetValue(type, out eventDataList);

            if(eventDataList == null)
            {
                return;
            }

            for(int i = 0; i < eventDataList.Count; ++i)
            {
                eventDataList[i].Action?.Invoke(obj);
            }
        }
    }
}
