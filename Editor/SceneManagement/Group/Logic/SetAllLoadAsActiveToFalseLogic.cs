namespace Juce.CoreUnity.SceneManagement.Group.Logic
{
    public static class SetAllLoadAsActiveToFalseLogic
    {
        public static void Execute(SceneGroup sceneGroup)
        {
            foreach(SceneGroupEntry entry in sceneGroup.Entries)
            {
                entry.LoadAsActive = false;
            }
        }
    }
}
