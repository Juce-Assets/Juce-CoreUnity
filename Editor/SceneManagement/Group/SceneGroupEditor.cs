using Juce.CoreUnity.SceneManagement.Group.Logic;
using UnityEditor;
using UnityEngine;
using Juce.CoreUnity.SceneManagement.Group.Data;
using Juce.CoreUnity.SceneManagement.Group.Drawers;
using Juce.CoreUnity.SceneManagement.Group.Helpers;

namespace Juce.CoreUnity.SceneManagement.Group
{
    [CustomEditor(typeof(SceneGroup))]
    public class SceneGroupEditor : Editor
    {
        private readonly ToolData toolData = new ToolData();

        private readonly ReorderableHelper reorderableHelper = new ReorderableHelper();

        private SceneGroup ActualTarget { get; set; }

        private void OnEnable()
        {
            ActualTarget = (SceneGroup)target;

            GatherSceneGroupCustomDrawersLogic.Execute(
                toolData
                );

            GatherSceneEntryCustomDrawersLogic.Execute(
                toolData
                );
        }

        public override void OnInspectorGUI()
        {
            LoadAsActiveChangeCheckLogic.Execute(toolData, ActualTarget);

            EditorGUI.BeginChangeCheck();

            serializedObject.Update();

            HeaderDrawer.Draw(ActualTarget);

            EditorGUILayout.Space(4);

            SceneEntriesDrawer.Draw(
                this,
                ActualTarget, 
                toolData,
                reorderableHelper
                );

            EditorGUILayout.Space(2);

            AddEntriesDrawer.Draw(ActualTarget);

            EditorGUILayout.Space(2);

            OpenCloseDrawer.Draw(ActualTarget);

            SceneGroupCustomDrawersDrawer.Draw(toolData, ActualTarget);

            if (Event.current.type != EventType.Layout)
            {
                ActuallyRemoveSceneEntriesLogic.Execute(toolData, ActualTarget);
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(ActualTarget);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
