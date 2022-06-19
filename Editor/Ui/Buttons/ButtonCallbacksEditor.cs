using UnityEditor;

namespace Juce.CoreUnity.Ui.Buttons
{
    [CustomEditor(typeof(ButtonCallbacks))]
    public class ButtonCallbacksEditor : Editor
    {
        private SerializedProperty triggerPointerUpOnPointerExitProperty;
        private SerializedProperty navigationProperty;

        private void OnEnable()
        {
            GatherSerializedProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(triggerPointerUpOnPointerExitProperty);
            EditorGUILayout.PropertyField(navigationProperty);

            serializedObject.ApplyModifiedProperties();
        }

        private void GatherSerializedProperties()
        {
            triggerPointerUpOnPointerExitProperty = serializedObject.FindProperty("triggerPointerUpOnPointerExit");
            navigationProperty = serializedObject.FindProperty("m_Navigation");
        }
    }
}