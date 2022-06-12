using Juce.Core.Validation;
using Juce.Core.Validation.Data;
using Juce.Core.Validation.Enums;
using Juce.Core.Validation.Results;
using System.Linq;
using UnityEditor;

namespace Juce.CoreUnity.Validation
{
    public static class ValidationEditorUtils
    {
        public static void DrawValidationResult(IValidationResult validationResult)
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Validation", EditorStyles.boldLabel);
                EditorGUILayout.LabelField($"Result: {validationResult.ValidationResultType}");

                IOrderedEnumerable<IValidationLog> validationLogs = validationResult.ValidationLogs.OrderBy(i => i.ValidationLogType);

                foreach (IValidationLog validationLog in validationLogs)
                {
                    switch (validationLog.ValidationLogType)
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
