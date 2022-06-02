using Juce.Loc.Dawers;
using UnityEditor;

namespace Juce.Loc.Configuration
{
    [CustomEditor(typeof(LocalizationToolConfiguration))]
    public class LocalizationToolConfigurationEditor : Editor
    {
        private LocalizationToolConfiguration actualTarget;

        private void OnEnable()
        {
            actualTarget = (LocalizationToolConfiguration)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            RefreshButtonDrawer.Draw(actualTarget);
        }
    }
}
