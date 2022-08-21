namespace Juce.CoreUnity.SceneManagement.Collections
{
    public interface ISceneCollectionEntry
    {
        string ScenePath { get; }
        bool LoadAsActive { get; }
    }
}
