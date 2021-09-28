using Juce.Core.DI.Builder;
using Juce.Core.DI.Container;
using System;
using UnityEngine;

namespace JuceUnity.Core.DI.Extensions
{
    public static class DIGameObjectExtensions
    {
        public static IDIBindingActionBuilder<T> FromGameObject<T>(
            this IDIBindingBuilder<T> builder, 
            GameObject gameObject
            ) where T : MonoBehaviour
        {
            Func<IDIResolveContainer, T> function = (IDIResolveContainer resolver) =>
            {
                Type type = typeof(T);

                if(gameObject == null)
                {
                    throw new Exception($"Tried to bind {type.Name} from {nameof(GameObject)}, " +
                        $"but {nameof(GameObject)} was null");
                }

                T foundObject = gameObject.GetComponent<T>();

                if(foundObject == null)
                {
                    throw new Exception($"Tried to bind {type.Name} from {nameof(GameObject)}, " +
                        $"but the {nameof(MonoBehaviour)} could not be found");
                }

                return foundObject;
            };

            return builder.FromFunction(function);
        }

        public static IDIBindingActionBuilder<T> FromGameObjectInChildren<T>(
            this IDIBindingBuilder<T> builder,
            GameObject gameObject
            ) where T : MonoBehaviour
        {
            Func<IDIResolveContainer, T> function = (IDIResolveContainer resolver) =>
            {
                Type type = typeof(T);

                if (gameObject == null)
                {
                    throw new Exception($"Tried to bind {type.Name} from {nameof(GameObject)}, " +
                        $"but {nameof(GameObject)} was null");
                }

                T foundObject = gameObject.GetComponentInChildren<T>();

                if (foundObject == null)
                {
                    throw new Exception($"Tried to bind {type.Name} from {nameof(GameObject)}, " +
                        $"but the {nameof(MonoBehaviour)} could not be found");
                }

                return foundObject;
            };

            return builder.FromFunction(function);
        }
    }
}
