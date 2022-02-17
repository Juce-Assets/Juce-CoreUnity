using Juce.Core.Tickable;

namespace Juce.CoreUnity.Tickables
{
    public interface ITickablesService 
    {
        void Add(ITickable tickable);
        void Remove(ITickable tickable);
    }
}