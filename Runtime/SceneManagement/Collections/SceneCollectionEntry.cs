namespace Juce.CoreUnity.SceneManagement.Collections
{
    public readonly struct SceneCollectionEntry : ISceneCollectionEntry
    {
        public string ScenePath { get; }
        public bool LoadAsActive { get; }

        public SceneCollectionEntry(
            string scenePath,
            bool loadAsActive
            )
        {
            ScenePath = scenePath;
            LoadAsActive = loadAsActive;
        }
    }
}
