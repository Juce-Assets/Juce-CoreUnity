using Juce.Core.Di.Builder;
using Juce.Core.Tickable;
using Juce.CoreUnity.Tickables;

namespace JuceUnity.Core.Di.Extensions
{
    public static class DiTickablesExtensions
    {
        public static IDiBindingActionBuilder<T> LinkToTickablesService<T>(this IDiBindingActionBuilder<T> actionBuilder)
            where T : ITickable
        {
            actionBuilder.WhenInit((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.Add(o);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.Remove(o);
            });

            return actionBuilder;
        }
    }
}
