using Juce.Core.Refresh;
using Juce.CoreUnity.Ui.ViewStack.Enums;

namespace Juce.CoreUnity.Ui.ViewStack.Entries
{
    public class ViewStackEntryRefresh
    {
        public RefreshType RefreshType { get; }
        public IRefreshable Refreshable { get; }

        public ViewStackEntryRefresh(
            RefreshType refreshType,
            IRefreshable refreshable
            )
        {
            RefreshType = refreshType;
            Refreshable = refreshable;
        }
    }
}
