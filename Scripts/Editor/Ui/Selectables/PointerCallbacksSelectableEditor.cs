using UnityEditor;

namespace Juce.CoreUnity.Ui
{
    [CustomEditor(typeof(PointerCallbacksSelectable))]
    public class PointerCallbacksSelectableEditor : Editor
    {
        private SerializedProperty pointerCallbacksProperty;
        private SerializedProperty navigationProperty;

        private void OnEnable()
        {
            GatherSerializedProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(pointerCallbacksProperty);
            EditorGUILayout.PropertyField(navigationProperty);
        }

        private void GatherSerializedProperties()
        {
            pointerCallbacksProperty = serializedObject.FindProperty("pointerCallbacks");
            navigationProperty = serializedObject.FindProperty("m_Navigation");
        }
    }
}