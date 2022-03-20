using UnityEditor;

namespace Juce.CoreUnity.Layers
{
    [CustomEditor(typeof(ParticleSystemLayerSelector))]
    public class ParticleSystemLayerSelectorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            LayerSelectorEditorUtils.DrawSetLayerButton();
        }
    }
}
