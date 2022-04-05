using Juce.Core.Di.Builder;
using Juce.Core.Subscribables;

namespace Juce.Core.Di.Extensions
{
    public static class DiViewStackExtensions
    {
        public static IDiBindingActionBuilder<T> LinkToViewStack<T>(this IDiBindingActionBuilder<T> actionBuilder)
            where T : ISubscribable
        {
            actionBuilder.WhenInit((c, o) => o.Subscribe());
            actionBuilder.WhenDispose((o) => o.Unsubscribe());

            return actionBuilder;
        }
    }
}
