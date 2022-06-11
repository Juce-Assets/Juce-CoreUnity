using UnityEngine;

namespace Juce.Extensions
{
    public static class ComponentExtensions
    {
        public static void Destroy(this Component component)
        {
            Object.Destroy(component);
        }

        public static void DestroyGameObject(this Component component)
        {
            component.gameObject.Destroy();
        }
    }
}
