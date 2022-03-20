using UnityEditor;
using UnityEngine;

namespace Juce.CoreUnity.Layers
{
    public class LayerSelectorEditorUtils : Editor
    {
        public static void DrawSetLayerButton()
        {
            if(Application.isPlaying)
            {
                return;
            }

            if(GUILayout.Button("Set Layer"))
            {
                LayerSelector[] layerSelectors = Resources.FindObjectsOfTypeAll<LayerSelector>();

                foreach(LayerSelector layerSelector in layerSelectors)
                {
                    layerSelector.SetLayer();
                }
            }
        }
    }
}