using Juce.Utils.Singletons;
using System;
using System.Collections.Generic;

namespace Juce.CoreUnity.Service
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public void Register<T>(T service)
        {
            Type type = typeof(T);

            bool alreadyAdded = services.ContainsKey(type);

            if(alreadyAdded)
            {
                throw new System.Exception($"Type {type} already added at {nameof(ServiceLocator)}");
            }

            services.Add(type, service);
        }

        public void Unregister<T>()
        {
            Type type = typeof(T);

            bool alreadyAdded = services.ContainsKey(type);

            if (!alreadyAdded)
            {
                throw new System.Exception($"Type {type} not found at {nameof(ServiceLocator)}");
            }

            services.Remove(type);
        }

        public T Get<T>() 
        {
            Type type = typeof(T);

            bool found = services.TryGetValue(type, out object value);

            if(!found)
            {
                throw new System.Exception($"Type {type} coulg not be found at {nameof(ServiceLocator)}");
            }

            return (T)value;
        }
    }
}