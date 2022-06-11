using UnityEngine;

namespace Juce.Extensions
{
    public static class GameObjectExtensions
    {
        public static void Destroy(this GameObject gameObject)
        {
            Object.Destroy(gameObject);
        }

        public static void DestroyImmediate(this GameObject gameObject)
        {
            Object.DestroyImmediate(gameObject);
        }

        public static bool TryGetComponent<T>(this GameObject gameObject, out T component) where T : Component
        {
            component = gameObject.GetComponent<T>();

            return component != null;
        }

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            bool found = gameObject.TryGetComponent(out T component);

            if (found)
            {
                return component;
            }

            return gameObject.AddComponent<T>();
        }

        public static void SetParent(this GameObject gameObject, GameObject parent, bool worldPositionStays = true)
        {
            gameObject.transform.SetParent(parent == null ? null : parent.transform, worldPositionStays);
        }

        public static void SetParent(this GameObject gameObject, Transform parent, bool worldPositionStays = true)
        {
            gameObject.transform.SetParent(parent, worldPositionStays);
        }

        public static void RemoveParent(this GameObject gameObject, bool worldPositionStays = true)
        {
            gameObject.transform.SetParent(null, worldPositionStays);
        }
    }
}