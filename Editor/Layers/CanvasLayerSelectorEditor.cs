using UnityEditor;

namespace Juce.CoreUnity.Layers
{
    [CustomEditor(typeof(CanvasLayerSelector))]
    public class CanvasLayerSelectorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            LayerSelectorEditorUtils.DrawSetLayerButton();
        }
    }
}
