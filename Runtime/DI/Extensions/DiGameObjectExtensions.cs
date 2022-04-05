using Juce.Core.Di.Builder;
using Juce.Core.Di.Container;
using System;
using UnityEngine;

namespace JuceUnity.Core.DI.Extensions
{
    public static class DiGameObjectExtensions
    {
        public static IDiBindingActionBuilder<T> FromGameObject<T>(
            this IDiBindingBuilder<T> builder, 
            GameObject gameObject
            ) where T : MonoBehaviour
        {
            Func<IDiResolveContainer, T> function = (IDiResolveContainer resolver) =>
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

        public static IDiBindingActionBuilder<T> FromGameObjectInChildren<T>(
            this IDiBindingBuilder<T> builder,
            GameObject gameObject
            ) where T : MonoBehaviour
        {
            Func<IDiResolveContainer, T> function = (IDiResolveContainer resolver) =>
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
