using System;
using UnityEditor;
using UnityEngine;

namespace Juce.CoreUnity.Serialization.SerializableTypes
{
    /// <summary>
    /// Property drawer for SerializableGuid
    ///
    /// Author: Searous
    /// </summary>
    [CustomPropertyDrawer(typeof(SerializableGuid))]
    public class SerializableGuidPropertyDrawer : PropertyDrawer
    {
        private const float SeparationY = 20;

        private float buttonSize;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Start property draw
            EditorGUI.BeginProperty(position, label, property);

            // Get property
            SerializedProperty serializedGuid = property.FindPropertyRelative("serializedGuid");

            // Draw label
            position = EditorGUI.PrefixLabel(
                new Rect(position.x, position.y + SeparationY / 2, position.width, position.height),
                GUIUtility.GetControlID(FocusType.Passive),
                label);

            position.y -= SeparationY / 2; // Offsets position so we can draw the label for the field centered

            buttonSize = position.width / 3; // Update size of buttons to always fit perfeftly above the string representation field

            // Buttons
            if (GUI.Button(new Rect(position.xMin, position.yMin, buttonSize, SeparationY - 2), "New"))
            {
                serializedGuid.stringValue = Guid.NewGuid().ToString();
            }

            if (GUI.Button(new Rect(position.xMin + buttonSize, position.yMin, buttonSize, SeparationY - 2), "Copy"))
            {
                EditorGUIUtility.systemCopyBuffer = serializedGuid.stringValue;
            }

            if (GUI.Button(new Rect(position.xMin + buttonSize * 2, position.yMin, buttonSize, SeparationY - 2), "Empty"))
            {
                serializedGuid.stringValue = Guid.Empty.ToString();
            }

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            Rect pos = new Rect(position.xMin, position.yMin + SeparationY, position.width, SeparationY - 2);
            EditorGUI.PropertyField(pos, serializedGuid, GUIContent.none);

            // End property
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return SeparationY * 2;
        }
    }
}
