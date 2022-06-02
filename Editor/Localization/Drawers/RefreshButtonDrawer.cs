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
            TaskResult<List<ValueRange>> spreadsheetValues = await TryDownloadSpreadsheetFromGoogleQuery.Execute(
                localizationToolConfiguration,
                CancellationToken.None
                );

            if(!spreadsheetValues.HasResult)
            {
                return;
            }

            TaskResult<LocalizationData> localizationData = GenerateLocalizationDataFromGoogleSpreadsheetQuery.Execute(
                localizationToolConfiguration,
                spreadsheetValues.Value
                );

            if(!localizationData.HasResult)
            {
                return;
            }

            bool success = await SaveLocalizationDataToDiskQuery.Execute(localizationData.Value, CancellationToken.None);

            if(success)
            {
                UnityEngine.Debug.Log($"New localization data updated successfully");
            }
        }
    }
}
