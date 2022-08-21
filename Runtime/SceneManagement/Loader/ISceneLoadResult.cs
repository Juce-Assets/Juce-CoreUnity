using UnityEngine.SceneManagement;

namespace Juce.CoreUnity.SceneManagement.Loader
{
    public interface ISceneLoadResult
    {
        bool Success { get; }
        Scene Scene { get; }
    }
}
