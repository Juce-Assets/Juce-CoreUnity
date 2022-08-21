using UnityEngine.SceneManagement;

namespace Juce.CoreUnity.SceneManagement.Loader
{
    public class SceneLoadResult : ISceneLoadResult
    {
        public bool Success { get; }
        public Scene Scene { get; }

        public SceneLoadResult()
        {
            Success = false;
            Scene = default;
        }

        public SceneLoadResult(Scene scene)
        {
            Success = true;
            Scene = scene;
        }
    }
}
