using Juce.CoreUnity.ViewStack.Entries;
using Juce.CoreUnity.ViewStack.Builder;

namespace Juce.CoreUnity.ViewStack
{
    public interface IUiViewStack
    {
        void Register(IViewStackEntry entry);
        void Unregister(IViewStackEntry entry);

        IViewStackSequenceBuilder New();
    }
}
