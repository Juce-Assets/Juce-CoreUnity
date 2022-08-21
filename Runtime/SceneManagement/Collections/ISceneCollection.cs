using System.Collections.Generic;

namespace Juce.CoreUnity.SceneManagement.Collections
{
    public interface ISceneCollection
    {
        IReadOnlyList<ISceneCollectionEntry> SceneEntries { get; }
    }
}
