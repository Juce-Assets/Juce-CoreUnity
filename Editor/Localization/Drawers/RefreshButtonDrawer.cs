using Google.Apis.Sheets.v4.Data;
using Juce.Core.Results;
using Juce.CoreUnity.Localization.Configuration;
using Juce.CoreUnity.Localization.Data;
using Juce.CoreUnity.Localization.Dawers;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Juce.CoreUnity.Localization.Dawers
{
    public static class RefreshButtonDrawer
    {
        public static void Draw(LocalizationToolConfiguration localizationToolConfiguration)
        {
            if (GUILayout.Button("Update spreadsheet"))
            {
                ExecuteRefreshQuery(localizationToolConfiguration);
            }
        }

        private static async void ExecuteRefreshQuery(LocalizationToolConfiguration localizationToolConfiguration)
        {
            ITaskResult<List<ValueRange>> spreadsheetValuesResult = await TryDownloadSpreadsheetFromGoogleQuery.Execute(
                localizationToolConfiguration,
                CancellationToken.None
                );

            bool hasSpreadsheetValues = spreadsheetValuesResult.TryGetResult(out List<ValueRange> spreadsheetValues);

            if (!hasSpreadsheetValues)
            {
                return;
            }

            ITaskResult<LocalizationData> localizationDataResult = GenerateLocalizationDataFromGoogleSpreadsheetQuery.Execute(
                localizationToolConfiguration,
                spreadsheetValues
                );

            bool hasLocalizationDataResult = localizationDataResult.TryGetResult(out LocalizationData localizationData);

            if (!hasLocalizationDataResult)
            {
                return;
            }

            bool success = await SaveLocalizationDataToDiskQuery.Execute(localizationData, CancellationToken.None);

            if(success)
            {
                UnityEngine.Debug.Log($"New localization data updated successfully");
            }
        }
    }
}
