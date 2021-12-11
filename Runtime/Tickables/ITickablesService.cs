using Juce.Core.Tickable;

namespace Juce.CoreUnity.Tickables
{
    public interface ITickablesService 
    {
        void AddTickable(ITickable tickable);
        void RemoveTickable(ITickable tickable);
    }
}