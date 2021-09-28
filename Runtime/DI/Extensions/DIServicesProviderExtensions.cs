using Juce.Core.DI.Builder;
using Juce.Core.DI.Container;
using Juce.CoreUnity.Service;
using System;

namespace JuceUnity.Core.DI.Extensions
{
    public static class DIServicesProviderExtensions
    {
        public static IDIBindingActionBuilder<T> FromServicesProvider<T>(this IDIBindingBuilder<T> builder) where T : IService
        {
            Func<IDIResolveContainer, T> function = (IDIResolveContainer resolver) =>
            {
                Type type = typeof(T);

                bool found = ServicesProvider.TryGetService(out T service);

                if (!found)
                {
                    throw new Exception($"Tried to bind {type.Name} from {nameof(ServicesProvider)}, " +
                        $"but it could not be found");
                }

                return service;
            };

            return builder.FromFunction(function);
        }
    }
}
