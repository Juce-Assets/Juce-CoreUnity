using Juce.Utils.Singletons;
using System;
using System.Collections.Generic;

namespace Juce.CoreUnity.Service
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            Type type = typeof(T);

            bool alreadyAdded = Instance.services.ContainsKey(type);

            if(alreadyAdded)
            {
                throw new System.Exception($"Type {type} already added at {nameof(ServiceLocator)}");
            }

            Instance.services.Add(type, service);
        }

        public static void Unregister<T>()
        {
            Type type = typeof(T);

            bool alreadyAdded = Instance.services.ContainsKey(type);

            if (!alreadyAdded)
            {
                throw new System.Exception($"Type {type} not found at {nameof(ServiceLocator)}");
            }

            Instance.services.Remove(type);
        }

        public static T Get<T>() 
        {
            Type type = typeof(T);

            bool found = Instance.services.TryGetValue(type, out object value);

            if(!found)
            {
                throw new System.Exception($"Type {type} could not be found at {nameof(ServiceLocator)}");
            }

            return (T)value;
        }
    }
}