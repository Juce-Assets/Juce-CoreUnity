using Juce.CoreUnity.SceneManagement.Group.CustomDrawers;
using Juce.CoreUnity.SceneManagement.Group.Data;

namespace Juce.CoreUnity.SceneManagement.Group.Drawers
{
    public static class SceneEntryCustomDrawersDrawer
    {
        public static void Draw(ToolData toolData, SceneGroup sceneGroup, SceneGroupEntry sceneGroupEntry)
        {
            foreach(ISceneEntryCustomDrawer customDrawer in toolData.SceneEntryCustomDrawers)
            {
                customDrawer.OnInspectorGUI(sceneGroup, sceneGroupEntry);
            }
        }
    }
}
