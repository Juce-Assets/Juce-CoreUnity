using UnityEngine.SceneManagement;

namespace Juce.CoreUnity.Scenes
{
    public class SceneLoadResult
    {
        public bool Success { get; }
        public Scene Scene { get; }

        public SceneLoadResult(bool success)
        {
            Success = success;
        }

        public SceneLoadResult(bool success, Scene scene)
        {
            Success = success;
            Scene = scene;
        }
    }
}
