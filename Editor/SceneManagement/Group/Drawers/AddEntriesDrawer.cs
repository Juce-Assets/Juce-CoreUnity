using Juce.CoreUnity.SceneManagement.Group.Logic;
using UnityEngine;

namespace Juce.CoreUnity.SceneManagement.Group.Drawers
{
    public static class AddEntriesDrawer
    {
        public static void Draw(
            SceneGroup sceneGroup
            )
        {
            if (GUILayout.Button("Add Scene"))
            {
                AddSceneEntryLogic.Execute(sceneGroup);
            }
        }
    }
}
