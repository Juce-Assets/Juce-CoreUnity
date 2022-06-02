using Juce.Loc.Constants;
using Juce.Loc.Data;
using Juce.Loc.Results;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.Loc.Requests
{
    public static class LoadLocalizationDataRequest
    {
        public static async Task<TaskResult<LocalizationData>> Execute()
        {
            ResourceRequest resourceRequest = Resources.LoadAsync(ConstantDirectories.FilePath);

            if (resourceRequest == null)
            {
                return TaskResult<LocalizationData>.FromError();
            }

            TaskCompletionSource<bool> taskCompletitionSource = new TaskCompletionSource<bool>();

            Action<AsyncOperation> setResult = (AsyncOperation asyncOperation) => taskCompletitionSource.SetResult(default);

            resourceRequest.completed += setResult;

            await taskCompletitionSource.Task;

            resourceRequest.completed -= setResult;

            TextAsset textAsset = resourceRequest.asset as TextAsset;

            if (textAsset == null)
            {
                return TaskResult<LocalizationData>.FromError();
            }

            LocalizationData localizationData;

            try
            {
                localizationData = JsonConvert.DeserializeObject<LocalizationData>(textAsset.text);
            }
            catch(Exception exception)
            {
                UnityEngine.Debug.LogError($"There was an error deserializing {nameof(LocalizationData)}, " +
                    $"at {nameof(LoadLocalizationDataRequest)}, " +
                    $"with the exception {exception.Message}");

                return TaskResult<LocalizationData>.FromError();
            }

            return TaskResult<LocalizationData>.FromResult(localizationData);
        }
    }
}
