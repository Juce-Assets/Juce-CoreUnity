using Juce.CoreUnity.SceneManagement.Group.Data;

namespace Juce.CoreUnity.SceneManagement.Group.Logic
{
    public static class RemoveSceneEntryLogic
    {
        public static void Execute(
            ToolData toolData,
            SceneGroupEntry sceneGroupEntry
            )
        {
            toolData.EntriesToRemove.Add(sceneGroupEntry);
        }
    }
}
