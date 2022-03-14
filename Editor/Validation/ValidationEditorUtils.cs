using Juce.Core.Validation;
using System.Linq;
using UnityEditor;

namespace Juce.CoreUnity.Validation
{
    public static class ValidationEditorUtils
    {
        public static void DrawValidationResult(ValidationResult validationResult)
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Validation", EditorStyles.boldLabel);
                EditorGUILayout.LabelField($"Result: {validationResult.ValidationResultType}");

                IOrderedEnumerable<ValidationLog> validationLogs = validationResult.ValidationLogs.OrderBy(i => i.LogType);

                foreach (ValidationLog validationLog in validationLogs)
                {
                    switch (validationLog.LogType)
                    {
                        case ValidationLogType.Info:
                            {
                                EditorGUILayout.HelpBox(validationLog.LogMessage, MessageType.Info);
                            }
                            break;

                        case ValidationLogType.Warning:
                            {
                                EditorGUILayout.HelpBox(validationLog.LogMessage, MessageType.Warning);
                            }
                            break;

                        case ValidationLogType.Error:
                            {
                                EditorGUILayout.HelpBox(validationLog.LogMessage, MessageType.Error);
                            }
                            break;
                    }
                }
            }
        }
    }
}
