using System;

namespace Juce.CoreUnity.Architecture
{
    public interface IEntityView<T>
    {
        T TypeId { get; }
        int InstanceId { get; }
    }
}