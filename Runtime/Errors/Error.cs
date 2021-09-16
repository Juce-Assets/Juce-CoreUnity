using System;
using System.Text;

namespace Juce.CoreUnity.Errors
{
    public static class Error
    {
        public static void CouldNotCreateByFactory(Type objectType, Type classType, string extraInfo = "")
        {
            StringBuilder stringBuilder = new StringBuilder(
                $"Object of type {nameof(objectType)} could not be created, " +
                $"at {nameof(classType)}");

            if(!string.IsNullOrEmpty(extraInfo))
            {
                stringBuilder.Append($" [{extraInfo}]");
            }

            UnityEngine.Debug.LogError(stringBuilder.ToString());
        }
    }
}
