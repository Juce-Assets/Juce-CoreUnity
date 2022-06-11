using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Animations
{
    public interface ITaskAnimation 
    {
        void Execute();
        Task Execute(CancellationToken cancellationToken);
        Task Execute(bool instantly, CancellationToken cancellationToken);
    }
}
