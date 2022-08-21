using Juce.CoreUnity.SceneManagement.Group.Data;

namespace Juce.CoreUnity.SceneManagement.Group.Logic
{
    public static class LoadAsActiveChangeCheckLogic
    {
        public static void Execute(
            ToolData toolData,
            SceneGroup sceneGroup
            )
        {
            foreach (SceneGroupEntry entry in sceneGroup.Entries)
            {
                bool found = toolData.LastUpdateSceneEntryLoadAsActiveMap.TryGetValue(
                    entry,
                    out bool lastState
                    );

                if(!found)
                {
                    continue;
                }

                if(lastState == entry.LoadAsActive || !entry.LoadAsActive)
                {
                    continue;
                }

                SetAllLoadAsActiveToFalseLogic.Execute(sceneGroup);
                entry.LoadAsActive = true;
            }

            toolData.LastUpdateSceneEntryLoadAsActiveMap.Clear();

            foreach (SceneGroupEntry entry in sceneGroup.Entries)
            {
                toolData.LastUpdateSceneEntryLoadAsActiveMap.Add(entry, entry.LoadAsActive);
            }
        }
    }
}
