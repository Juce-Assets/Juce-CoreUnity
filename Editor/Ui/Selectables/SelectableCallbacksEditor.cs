using UnityEditor;

namespace Juce.CoreUnity.Ui.SelectableCallback
{
    [CustomEditor(typeof(SelectableCallbacks))]
    public class SelectableCallbacksEditor : Editor
    {
        private SerializedProperty navigationProperty;

        private void OnEnable()
        {
            GatherSerializedProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(navigationProperty);

            serializedObject.ApplyModifiedProperties();
        }

        private void GatherSerializedProperties()
        {
            navigationProperty = serializedObject.FindProperty("m_Navigation");
        }
    }
}