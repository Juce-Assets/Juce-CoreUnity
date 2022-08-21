using Juce.Core.Di.Builder;
using Juce.Core.Di.Container;
using Juce.CoreUnity.Service;
using System;

namespace Juce.CoreUnity.Di.Extensions
{
    public static class DiServicesLocatorExtensions
    {
        public static IDiBindingActionBuilder<T> ToServicesLocator<T>(this IDiBindingActionBuilder<T> builder)
        {
            builder.WhenInit((c, o) => ServiceLocator.Register(builder.IdentifierType, o));
            builder.WhenDispose((c, o) => ServiceLocator.Unregister(builder.IdentifierType));

            return builder;
        }

        public static IDiBindingActionBuilder<T> FromServicesLocator<T>(this IDiBindingBuilder<T> builder)
        {
            Func<IDiResolveContainer, T> function = (IDiResolveContainer resolver) =>
            {
                return ServiceLocator.Get<T>(); 
            };

            return builder.FromFunction(function);
        }
    }
}
