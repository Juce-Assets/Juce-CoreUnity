using System;

namespace Juce.Core.Services
{
    public interface IUpdatableService : IService
    {
        void Update();
    }
}
