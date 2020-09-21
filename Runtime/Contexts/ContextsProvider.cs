using System;
using System.Collections.Generic;
using Juce.Utils.Contracts;
using Juce.Utils.Singletons;

namespace Juce.Core.Contexts
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
            Contract.IsNotNull(context, "Trying to register null context");

            bool alreadyExists = TryGetContext<T>(out _);

            Contract.IsFalse(alreadyExists, $"Context {nameof(T)} has been already added");

            allContexts.Add(context);
        }

        public void UnregisterContext(Context context)
        {
            Contract.IsNotNull(context, "Trying to unregister null context");

            bool found = allContexts.Remove(context);

            Contract.IsTrue(found, $"Trying  to unregister service {context.GetType().Name} but it was not registered");
        }

        public T GetContext<T>() where T : Context
        {
            T context;

            bool found = TryGetContext<T>(out context);

            Contract.IsTrue(found, $"Context {nameof(T)} could not be found");

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
