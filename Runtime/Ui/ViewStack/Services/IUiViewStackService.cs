using Juce.CoreUnity.Ui.ViewStack.Entries;
using Juce.CoreUnity.Ui.ViewStack.Builder;

namespace Juce.CoreUnity.ViewStack.Services
{
    public interface IUiViewStackService
    {
        void Register(IViewStackEntry entry);
        void Unregister(IViewStackEntry entry);

        void SetNotInteractableNow<T>();

        IViewStackSequenceBuilder New();
    }
}
