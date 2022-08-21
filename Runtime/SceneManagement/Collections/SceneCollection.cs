using System.Collections.Generic;

namespace Juce.CoreUnity.SceneManagement.Collections
{
    public readonly struct SceneCollection : ISceneCollection
    {
        public IReadOnlyList<ISceneCollectionEntry> SceneEntries { get; }

        public SceneCollection(IReadOnlyList<ISceneCollectionEntry> sceneEntries)
        {
            SceneEntries = sceneEntries;
        }

        public SceneCollection Merge(
            ISceneCollection sceneCollection1,
            ISceneCollection sceneCollection2
            )
        {
            List<ISceneCollectionEntry> sceneEntries = new List<ISceneCollectionEntry>();
            sceneEntries.AddRange(sceneCollection1.SceneEntries);
            sceneEntries.AddRange(sceneCollection2.SceneEntries);

            return new SceneCollection(sceneEntries);
        }
    }
}
