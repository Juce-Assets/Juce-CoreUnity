using UnityEditor;

namespace Juce.CoreUnity.SceneManagement.Group.Logic
{
    public static class GetEntriesPropertyLogic
    {
        public static SerializedProperty Execute(SceneGroupEditor sceneGroupEditor)
        {
            return sceneGroupEditor.serializedObject.FindProperty("Entries");
        }
    }
}
