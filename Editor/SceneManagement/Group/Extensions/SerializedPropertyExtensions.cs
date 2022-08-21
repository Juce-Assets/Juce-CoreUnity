using System;
using UnityEditor;

namespace Juce.CoreUnity.SceneManagement.Group.Extensions
{
    public static class SerializedPropertyExtensions
    {
        public static void ForeachVisibleChildren(
            this SerializedProperty serializedProperty,
            Action<SerializedProperty> action
            )
        {
            if (!serializedProperty.hasVisibleChildren)
            {
                return;
            }

            SerializedProperty endProperty = serializedProperty.GetEndProperty();

            serializedProperty.NextVisible(true);

            while (!SerializedProperty.EqualContents(serializedProperty, endProperty))
            {
                action.Invoke(serializedProperty);
                serializedProperty.NextVisible(false);
            }
        }

        public static void AddElementToArray(
            this SerializedProperty serializedProperty
            )
        {
            if(!serializedProperty.isArray)
            {
                return;
            }

            serializedProperty.arraySize++;
        }

        public static void RemoveElementFromArray(
            this SerializedProperty serializedProperty,
            int index
            )
        {
            if (!serializedProperty.isArray)
            {
                return;
            }

            if(serializedProperty.arraySize <= index)
            {
                return;
            }

            int oldSize = serializedProperty.arraySize;

            serializedProperty.DeleteArrayElementAtIndex(index);

            if (serializedProperty.arraySize == oldSize)
            {
                serializedProperty.DeleteArrayElementAtIndex(index);
            }
        }
    }
}