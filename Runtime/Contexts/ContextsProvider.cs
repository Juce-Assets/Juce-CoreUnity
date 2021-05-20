using Juce.Utils.Singletons;
using System;
using System.Collections.Generic;

namespace Juce.CoreUnity.Contexts
{
    public class ContextsProvider : AutoStartMonoSingleton<ContextsProvider>
    {
        private readonly List<Context> allContexts = new List<Context>();

        public static void Register<T>(T service) where T : Context
        {
            Instance.RegisterContext(service);
        }

        public static void Unregister(Context context)
        {
            Instance.UnregisterContext(context);
        }

        public void RegisterContext<T>(T context) where T : Context
        {
            if (context == null)
            {
                UnityEngine.Debug.LogError($"Trying to register null context at {nameof(ContextsProvider)}");
            }

            bool alreadyExists = TryGetContext<T>(out _);

            if (context == null)
            {
                UnityEngine.Debug.LogError($"Context {nameof(T)} has been already added at {nameof(ContextsProvider)}");
            }

            allContexts.Add(context);
        }

        public void UnregisterContext(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException($"Trying to unregister null contex at {nameof(ContextsProvider)}");
            }

            bool found = allContexts.Remove(context);

            if (!found)
            {
                throw new Exception($"Tried to unregister service {context.GetType().Name} but it was not registered at {nameof(ContextsProvider)}");
            }
        }

        public static T GetContext<T>() where T : Context
        {
            bool found = Instance.TryGetContext<T>(out T context);

            if (!found)
            {
                throw new Exception($"Context {nameof(T)} could not be found at {nameof(ContextsProvider)}");
            }

            return context;
        }

        private bool TryGetContext<T>(out T outContext) where T : Context
        {
            for (int i = 0; i < allContexts.Count; ++i)
            {
                if (allContexts[i].GetType() == typeof(T))
                {
                    outContext = (T)allContexts[i];
                    return true;
                }
            }

            outContext = default;
            return false;
        }
    }
}