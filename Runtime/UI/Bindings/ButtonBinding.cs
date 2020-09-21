using System;
using UnityEngine.UI;
using Juce.Core.Contracts;

namespace Juce.Core.UI
{
    public class ButtonBinding : Binding
    {
        private readonly Button button;

        private Action onClick;

        public ButtonBinding(Button button, Action onClick)
        {
            Contract.IsNotNull(button, $"{nameof(Button)} null at {nameof(ButtonBinding)}");

            this.button = button;
            this.onClick = onClick;
        }

        protected override void OnBind()
        {
            button.onClick.AddListener(OnClick);
        }

        protected override void OnUnbind()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            onClick?.Invoke();
        }
    }
}
