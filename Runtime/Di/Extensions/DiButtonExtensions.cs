using System;
using Juce.Core.Di.Builder;
using UnityEngine.UI;

namespace Juce.CoreUnity.Di.Extensions
{
    public static class DiButtonExtensions
    {
        public static IDiBindingActionBuilder<T> LinkButton<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Button button,
            Func<T, Action> func
        )
        {
            Action action = null;

            actionBuilder.WhenInit((c, o) =>
            {
                action = func.Invoke(o);

                button.onClick.AddListener(action.Invoke);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                button.onClick.RemoveListener(action.Invoke);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }
    }
}
