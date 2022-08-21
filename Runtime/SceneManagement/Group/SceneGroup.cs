using Juce.CoreUnity.SceneManagement.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.SceneManagement.Group
{
    [CreateAssetMenu(fileName = "SceneGroup", menuName = "Juce/SceneManagement/SceneGroup", order = 1)]
    public class SceneGroup : ScriptableObject
    {
        [SerializeField] public List<SceneGroupEntry> Entries = new List<SceneGroupEntry>();

        private ISceneCollection cachedSceneCollection;

        public ISceneCollection SceneCollection => GenerateCollection();

        private ISceneCollection GenerateCollection()
        {
            if (!Application.isEditor)
            {
                if(cachedSceneCollection != null)
                {
                    return cachedSceneCollection;
                }
            }

            List<ISceneCollectionEntry> sceneEntries = new List<ISceneCollectionEntry>();

            foreach(SceneGroupEntry entry in Entries)
            {
                if(entry.SceneReference == null)
                {
                    UnityEngine.Debug.LogError($"Missing scene detected at {nameof(SceneGroup)} {name}");
                    continue;
                }

                if(string.IsNullOrEmpty(entry.SceneReference.ScenePath))
                {
                    UnityEngine.Debug.LogError($"Missing scene detected at {nameof(SceneGroup)} {name}");
                    continue;
                }

                SceneCollectionEntry sceneEntry = new SceneCollectionEntry(
                    entry.SceneReference.ScenePath,
                    entry.LoadAsActive
                    );

                sceneEntries.Add(sceneEntry);
            }

            cachedSceneCollection = new SceneCollection(sceneEntries);

            return cachedSceneCollection;
        }
    }
}
