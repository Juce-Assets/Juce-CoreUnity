using Juce.Core.Tick.Tickable;
using Juce.CoreUnity.Tick.Enums;

namespace Juce.CoreUnity.Tick.Services
{
    public interface ITickablesService
    {
        void Add(ITickable tickable, TickType tickType);
        void Remove(ITickable tickable, TickType tickType);
        void RemoveNow(ITickable tickable, TickType tickType);
    }
}
