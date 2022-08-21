using System;

namespace Juce.CoreUnity.SceneManagement.Group.Logic
{
    public static class ReorderSceneEntriesLogic
    {
        public static void Execute(SceneGroup sceneGroup, int componentIndex, int newComponentIndex)
        {
            if (componentIndex == newComponentIndex)
            {
                return;
            }

            if (componentIndex < 0 || componentIndex >= sceneGroup.Entries.Count)
            {
                return;
            }

            newComponentIndex = Math.Min(newComponentIndex, sceneGroup.Entries.Count - 1);
            newComponentIndex = Math.Max(newComponentIndex, 0);

            SceneGroupEntry entry = sceneGroup.Entries[componentIndex];

            sceneGroup.Entries.RemoveAt(componentIndex);
            sceneGroup.Entries.Insert(newComponentIndex, entry);
        }
    }
}
