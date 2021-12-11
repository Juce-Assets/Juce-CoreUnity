using Juce.Utils.Singletons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.Contexts
{
    public class ContextsProvider : AutoStartMonoSingleton<ContextsProvider>
    {
        private readonly Dictionary<Type, object> contexts = new Dictionary<Type, object>();

        public static void Register<T>(T context) 
        {
            if (InstanceWasDestroyed)
            {
                return;
            }

            if (context == null)
            {
                UnityEngine.Debug.LogError($"Trying to register null context at {nameof(ContextsProvider)}");
            }

            bool alreadyExists = TryGetContext<T>(out _);

            if (context == null)
            {
                UnityEngine.Debug.LogError($"Context {nameof(T)} has been already added at {nameof(ContextsProvider)}");
            }

            Type type = typeof(T);

            Instance.contexts.Add(type, context);
        }

        public static void Unregister<T>()
        {
            if(InstanceWasDestroyed)
            {
                return;
            }

            Type type = typeof(T);

            bool found = Instance.contexts.Remove(type);

            if (!found)
            {
                throw new Exception($"Tried to unregister service {type.Name} but it " +
                    $"was not registered at {nameof(ContextsProvider)}");
            }
        }

        public static T GetContext<T>()
        {
            if (InstanceWasDestroyed)
            {
                return default;
            }

            Type type = typeof(T);

            bool found = TryGetContext<T>(out T context);

            if (!found)
            {
                throw new Exception($"Context {nameof(T)} could not be found at {nameof(ContextsProvider)}");
            }

            return context;
        }

        public static bool TryGetContext<T>(out T context) 
        {
            if (InstanceWasDestroyed)
            {
                context = default;
                return false;
            }

            Type type = typeof(T);

            bool found = Instance.contexts.TryGetValue(type, out object foundContext);

            if (!found)
            {
                context = default;
                return false;
            }

            context = (T)foundContext;
            return true;
        }
    }
}