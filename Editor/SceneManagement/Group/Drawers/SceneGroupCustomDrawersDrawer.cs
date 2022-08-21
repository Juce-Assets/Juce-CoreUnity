using Juce.CoreUnity.SceneManagement.Group.CustomDrawers;
using Juce.CoreUnity.SceneManagement.Group.Data;

namespace Juce.CoreUnity.SceneManagement.Group.Drawers
{
    public static class SceneGroupCustomDrawersDrawer
    {
        public static void Draw(ToolData toolData, SceneGroup sceneGroup)
        {
            foreach (ISceneGroupCustomDrawer customDrawer in toolData.SceneGroupCustomDrawer)
            {
                customDrawer.OnInspectorGUI(sceneGroup);
            }
        }
    }
}
