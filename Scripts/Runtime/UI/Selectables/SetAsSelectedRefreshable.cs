using Juce.Core.Refresh;
using Juce.CoreUnity.Ui.Others.Navigation;
using UnityEngine.EventSystems;

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
            if(!EventSystem.current.alreadySelecting)
            {
                if(InputSystemUIInputModuleNavigationExtension.Current == null)
                {
                    return;
                }

                InputSystemUIInputModuleNavigationExtension.Current.SetFallbackSelectable(selectableCallbacks);

                return;
            }

            selectableCallbacks.SetAsSelected();
        }
    }
}
