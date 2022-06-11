using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Persistence.Serialization
{
    public interface ISerializableData<T> where T : class
    {
        event Action<T> OnSave;
        event Action<T, bool> OnLoad;

        T Data { get; }

        Task Save(CancellationToken cancellationToken);
        Task Load(CancellationToken cancellationToken);

    }
}