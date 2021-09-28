using Juce.Core.DI.Builder;
using Juce.Core.DI.Container;
using Juce.CoreUnity.Contexts;
using System;

namespace JuceUnity.Core.DI.Extensions
{
    public static class DIContextsProviderExtensions
    {
        public static IDIBindingActionBuilder<T> FromContextsProvider<T>(this IDIBindingBuilder<T> builder) where T : Context
        {
            Func<IDIResolveContainer, T> function = (IDIResolveContainer resolver) =>
            {
                Type type = typeof(T);

                bool found = ContextsProvider.TryGetContext(out T context);

                if (!found)
                {
                    throw new Exception($"Tried to bind {type.Name} from {nameof(ContextsProvider)}, " +
                        $"but it could not be found");
                }

                return context;
            };

            return builder.FromFunction(function);
        }
    }
}
