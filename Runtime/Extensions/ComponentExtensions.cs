using UnityEngine;

namespace Juce.Extensions
{
    public static class ComponentExtensions
    {
        public static void DestroyGameObject(this Component component)
        {
            if (component == null)
            {
                return;
            }

            MonoBehaviour.Destroy(component.gameObject);
        }

        public static T InstantiateGameObjectAndGetComponent<T>(this T component, Transform parent = null) where T : Component
        {
            GameObject instance = MonoBehaviour.Instantiate(component.gameObject, parent);

            return instance.GetComponent<T>();
        }
    }
}
