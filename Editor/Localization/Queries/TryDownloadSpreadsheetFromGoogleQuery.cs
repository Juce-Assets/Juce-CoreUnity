using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Juce.Loc.Configuration;
using Juce.Loc.Results;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Loc.Dawers
{
    public static class TryDownloadSpreadsheetFromGoogleQuery
    {
        public static async Task<TaskResult<List<ValueRange>>> Execute(
            LocalizationToolConfiguration localizationToolConfiguration,
            CancellationToken cancellationToken
            )
        {
            UnityEngine.Debug.Log($"Start downloading Spreadsheet from key: {localizationToolConfiguration.SpreadsheetKey}");

            UnityEngine.Debug.Log($"1.Authenticating");

            bool serviceFound = TryGetSheetsServiceFromCredentials.Execute(
                localizationToolConfiguration,
                out SheetsService sheetsService
                );

            if(!serviceFound)
            {
                UnityEngine.Debug.Log($"Query can't continue, Sheets Service not found");
                return TaskResult<List<ValueRange>>.FromError();
            }

            UnityEngine.Debug.Log($"2.Downloading spreadsheets...");

            SpreadsheetsResource.GetRequest spreadsheetsResource = sheetsService.Spreadsheets.Get(
                localizationToolConfiguration.SpreadsheetKey
                );

            Spreadsheet spreadSheetData;

            try
            {
                spreadSheetData = await spreadsheetsResource.ExecuteAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.Log($"There was an error downloading spreadsheet: {exception.Message}");
                return TaskResult<List<ValueRange>>.FromError();
            }

            if(spreadSheetData == null)
            {
                UnityEngine.Debug.Log($"Query can't continue, spreadsheets could not be found");
                return TaskResult<List<ValueRange>>.FromError();
            }

            UnityEngine.Debug.Log($"3.Spreadsheet downloaded: {spreadSheetData.Properties.Title}");

            IList<Sheet> sheets = spreadSheetData.Sheets;

            if (sheets == null)
            {
                sheets = new List<Sheet>();
            }

            UnityEngine.Debug.Log($"4.Spreadsheet downloading values... ");

            List<ValueRange> values = new List<ValueRange>();

            foreach (Sheet sheet in sheets)
            {
                SpreadsheetsResource.ValuesResource.BatchGetRequest request = sheetsService.Spreadsheets.Values.BatchGet(
                    localizationToolConfiguration.SpreadsheetKey
                    );

                request.Ranges = new List<string>() { sheet.Properties.Title };

                BatchGetValuesResponse response;

                try
                {
                    response = await request.ExecuteAsync(cancellationToken);
                }
                catch (Exception exception)
                {
                    UnityEngine.Debug.Log($"There was an error downloading spreadsheet values: {exception.Message}");
                    continue;
                }

                values.AddRange(response.ValueRanges);
            }

            UnityEngine.Debug.Log($"5.Spreadsheet values downloaded");

            return TaskResult<List<ValueRange>>.FromResult(values);
        }
	}
}
