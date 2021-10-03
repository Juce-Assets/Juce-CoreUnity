using Juce.Core.Disposables;
using Juce.Core.Factories;
using UnityEngine;

namespace Juce.CoreUnity.Factories
{
    public abstract class MonoBehaviourKnownPrefabFactory<TDefinition, TCreation> 
        : IFactory<TDefinition, IDisposable<TCreation>> where TCreation : MonoBehaviour
    {
        private readonly TCreation prefab;
        private readonly Transform parent;

        public MonoBehaviourKnownPrefabFactory(TCreation prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
        }

        public bool TryCreate(TDefinition definition, out IDisposable<TCreation> creation)
        {
            TCreation instance = Object.Instantiate(prefab, parent);

            if(instance == null)
            {
                creation = default;
                return false;
            }

            Init(definition, instance);

            creation = new Disposable<TCreation>(
                instance, 
                Dispose
                );

            return true;
        }

        private void Dispose(TCreation toDispose)
        {
            if(toDispose == null)
            {
                return;
            }

            CleanUp(toDispose);

            Object.Destroy(toDispose.gameObject);
        }

        protected abstract void Init(TDefinition definition, TCreation creation);
        protected virtual void CleanUp(TCreation toDispose) { }
    }
}