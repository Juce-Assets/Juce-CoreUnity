using Juce.Core.Di.Builder;
using Juce.CoreUnity.ViewStack;
using Juce.CoreUnity.ViewStack.Entries;

namespace JuceUnity.Core.Di.Extensions
{
    public static class DiViewStackExtensions
    {
        public static IDiBindingActionBuilder<T> LinkToViewStack<T>(this IDiBindingActionBuilder<T> actionBuilder)
            where T : IViewStackEntry
        {
            actionBuilder.WhenInit((c, o) =>
            {
                IUiViewStack uiViewStack = c.Resolve<IUiViewStack>();

                uiViewStack.Register(o);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                IUiViewStack uiViewStack = c.Resolve<IUiViewStack>();

                uiViewStack.Unregister(o);
            });

            return actionBuilder;
        }
    }
}
