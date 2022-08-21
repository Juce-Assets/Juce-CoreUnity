namespace Juce.CoreUnity.SceneManagement.Group.CustomDrawers
{
    public interface ISceneEntryCustomDrawer
    {
        void OnInspectorGUI(
            SceneGroup sceneGroup,
            SceneGroupEntry sceneGroupEntry
            );
    }
}
