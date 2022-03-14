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

            Register(type, service);
        }

        public static void Register(Type type, object service)
        {
            bool alreadyAdded = Instance.services.ContainsKey(type);

            if (alreadyAdded)
            {
                throw new System.Exception($"Type {type} already added at {nameof(ServiceLocator)}");
            }

            Instance.services.Add(type, service);
        }

        public static void Unregister<T>()
        {
            Type type = typeof(T);

            Unregister(type);
        }

        public static void Unregister(Type type)
        {
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

        public static bool TryGet<T>(out T item)
        {
            Type type = typeof(T);

            bool found = Instance.services.TryGetValue(type, out object value);

            if (!found)
            {
                item = default;
                return false;
            }

            item = (T)value;
            return true;
        }

        public static bool Has<T>()
        {
            Type type = typeof(T);

            return Instance.services.TryGetValue(type, out _); ;
        }
    }
}