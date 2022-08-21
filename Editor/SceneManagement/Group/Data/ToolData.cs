using Juce.CoreUnity.SceneManagement.Group.CustomDrawers;
using System.Collections.Generic;

namespace Juce.CoreUnity.SceneManagement.Group.Data
{
    public class ToolData
    {
        public List<SceneGroupEntry> EntriesToRemove { get; } = new List<SceneGroupEntry>();
        public Dictionary<SceneGroupEntry, bool> LastUpdateSceneEntryLoadAsActiveMap { get; } = new Dictionary<SceneGroupEntry, bool>();

        public List<ISceneEntryCustomDrawer> SceneEntryCustomDrawers { get; } = new List<ISceneEntryCustomDrawer>();
        public List<ISceneGroupCustomDrawer> SceneGroupCustomDrawer { get; } = new List<ISceneGroupCustomDrawer>();
    }
}
