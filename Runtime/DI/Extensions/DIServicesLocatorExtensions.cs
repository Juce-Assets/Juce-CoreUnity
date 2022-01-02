using Juce.Core.DI.Builder;
using Juce.Core.DI.Container;
using Juce.CoreUnity.Service;
using System;

namespace JuceUnity.Core.DI.Extensions
{
    public static class DIServicesLocatorExtensions
    {
        public static IDIBindingActionBuilder<T> ToServicesLocator<T>(this IDIBindingActionBuilder<T> builder)
        {
            builder.WhenInit((c, o) => ServiceLocator.Register(o));
            builder.WhenDispose((c, o) => ServiceLocator.Unregister<T>());

            return builder;
        }

        public static IDIBindingActionBuilder<T> FromServicesLocator<T>(this IDIBindingBuilder<T> builder)
        {
            Func<IDIResolveContainer, T> function = (IDIResolveContainer resolver) =>
            {
                return ServiceLocator.Get<T>(); 
            };

            return builder.FromFunction(function);
        }
    }
}
