using Juce.CoreUnity.SceneManagement.Group.Data;
using UnityEditor;

namespace Juce.CoreUnity.SceneManagement.Group.Logic
{
    public static class ActuallyRemoveSceneEntriesLogic
    {
        public static void Execute(
            ToolData toolData,
            SceneGroup sceneGroup
            )
        {
            bool willRemove = toolData.EntriesToRemove.Count > 0;

            foreach(SceneGroupEntry toRemove in toolData.EntriesToRemove)
            {
                sceneGroup.Entries.Remove(toRemove);
            }

            toolData.EntriesToRemove.Clear();

            if(willRemove)
            {
                EditorUtility.SetDirty(sceneGroup);
            }
        }
    }
}
