using System.Threading.Tasks;

namespace Juce.CoreUnity.Contexts
{
    public interface IContextLoader
    {
        Task Load();

        Task Unload();
    }
}