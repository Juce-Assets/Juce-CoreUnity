using Juce.Core.Di.Builder;
using Juce.Core.Tick.Tickable;
using Juce.CoreUnity.Tick.Enums;
using Juce.CoreUnity.Tick.Services;
using System;

namespace Juce.CoreUnity.Di.Extensions
{
    public static class DiTickablesExtensions
    {
        public static IDiBindingActionBuilder<T> LinkToTickablesService<T>(
            this IDiBindingActionBuilder<T> actionBuilder, TickType tickType = TickType.Update
            )
            where T : ITickable
        {
            actionBuilder.WhenInit((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.Add(o, tickType);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.Remove(o, tickType);
            });

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkToTickablesService<T>(
            this IDiBindingActionBuilder<T> actionBuilder, 
            Func<T, Action> func,
            TickType tickType = TickType.Update
            )
        {
            CallbackTickable callbackTickable = null;

            actionBuilder.WhenInit((c, o) =>
            {
                Action action = func.Invoke(o);

                callbackTickable = new CallbackTickable(action);

                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.Add(callbackTickable, tickType);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.Remove(callbackTickable, tickType);
            });

            return actionBuilder;
        }
    }
}
