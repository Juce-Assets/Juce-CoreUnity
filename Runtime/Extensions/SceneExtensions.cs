using UnityEngine;
using UnityEngine.SceneManagement;

namespace Juce.CoreUnity.Extensions
{
    public static class SceneExtensions
    {
        public static bool TryGetRootComponent<T>(this Scene scene, out T component) where T : MonoBehaviour
        {
            if(!scene.isLoaded)
            {
                component = default;
                return false;
            }

            foreach (GameObject gameObject in scene.GetRootGameObjects())
            {
                bool found = gameObject.TryGetComponent(out component);

                if (!found)
                {
                    continue;
                }

                return true;
            }

            component = default;
            return false;
        }
    }
}
