using System;

namespace Juce.Core.Services
{
    public interface IService
    {
        void Init();
        void CleanUp();
    }
}
