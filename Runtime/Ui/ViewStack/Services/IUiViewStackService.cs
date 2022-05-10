using Juce.CoreUnity.ViewStack.Entries;
using Juce.CoreUnity.ViewStack.Builder;

namespace Juce.CoreUnity.ViewStack.Services
{
    public interface IUiViewStackService
    {
        void Register(IViewStackEntry entry);
        void Unregister(IViewStackEntry entry);

        IViewStackSequenceBuilder New();
    }
}
