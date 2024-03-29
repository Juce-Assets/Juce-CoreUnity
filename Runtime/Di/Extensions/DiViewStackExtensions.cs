﻿using Juce.Core.Di.Builder;
using Juce.CoreUnity.Ui.ViewStack.Entries;
using Juce.CoreUnity.ViewStack.Services;

namespace Juce.CoreUnity.Di.Extensions
{
    public static class DiViewStackExtensions
    {
        public static IDiBindingActionBuilder<T> LinkToViewStackService<T>(this IDiBindingActionBuilder<T> actionBuilder)
            where T : IViewStackEntry
        {
            actionBuilder.WhenInit((c, o) =>
            {
                IUiViewStackService uiViewStack = c.Resolve<IUiViewStackService>();

                uiViewStack.Register(o);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                IUiViewStackService uiViewStack = c.Resolve<IUiViewStackService>();

                uiViewStack.Unregister(o);
            });

            return actionBuilder;
        }
    }
}
