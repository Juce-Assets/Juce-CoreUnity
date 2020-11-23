using System;
using System.Threading.Tasks;

namespace Juce.Core.Contexts
{
    public interface IContextLoader
    {
        Task Load();

        Task Unload();
    }
}