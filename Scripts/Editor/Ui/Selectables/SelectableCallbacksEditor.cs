using UnityEditor;

namespace Juce.CoreUnity.Ui
{
    [CustomEditor(typeof(SelectableCallbacks))]
    public class SelectableCallbacksEditor : Editor
    {
        private SerializedProperty firstSelectedProperty;
        private SerializedProperty navigationProperty;

        private void OnEnable()
        {
            GatherSerializedProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(firstSelectedProperty);
            EditorGUILayout.PropertyField(navigationProperty);

            serializedObject.ApplyModifiedProperties();
        }

        private void GatherSerializedProperties()
        {
            firstSelectedProperty = serializedObject.FindProperty("firstSelected");
            navigationProperty = serializedObject.FindProperty("m_Navigation");
        }
    }
}