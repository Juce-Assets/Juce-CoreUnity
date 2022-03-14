using Juce.Core.Refresh;

namespace Juce.CoreUnity.ViewStack.Entries
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
