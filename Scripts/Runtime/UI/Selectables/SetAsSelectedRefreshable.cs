using Juce.Core.Refresh;

namespace Juce.CoreUnity.Ui.SelectableCallback
{
    public class SetAsSelectedRefreshable : IRefreshable
    {
        private readonly SelectableCallbacks selectableCallbacks;

        public SetAsSelectedRefreshable(
            SelectableCallbacks selectableCallbacks
            )
        {
            this.selectableCallbacks = selectableCallbacks;
        }

        public void Refresh()
        {
            selectableCallbacks.SetAsSelected();
        }
    }
}
