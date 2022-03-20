using UnityEditor;

namespace Juce.CoreUnity.Layers
{
    [CustomEditor(typeof(RendererLayerSelector))]
    public class RendererLayerSelectorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            LayerSelectorEditorUtils.DrawSetLayerButton();
        }
    }
}
