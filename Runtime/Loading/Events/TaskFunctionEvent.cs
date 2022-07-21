using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Loading.Events
{
    public delegate Task TaskFunctionEvent(bool instantly, CancellationToken cancellationToken);
}
