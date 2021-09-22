using UnityEngine;

namespace Juce.CoreUnity.Factories
{
    public interface MonoBehaviourUnknownPrefabFactoryDefinition<TCreation> where TCreation : MonoBehaviour
    {
        TCreation Prefab { get; }
    }
}