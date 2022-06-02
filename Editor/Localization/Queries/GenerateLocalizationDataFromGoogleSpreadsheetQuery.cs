using Google.Apis.Sheets.v4.Data;
using Juce.Loc.Configuration;
using Juce.Loc.Constants;
using Juce.Loc.Data;
using Juce.Loc.Results;
using System.Collections.Generic;

namespace Juce.Loc.Dawers
{
    public static class GenerateLocalizationDataFromGoogleSpreadsheetQuery
    {
        public static TaskResult<LocalizationData> Execute(
            LocalizationToolConfiguration localizationToolConfiguration,
            IList<ValueRange> ranges
            )
        {
            List<LanguageLocalizationData> languageValues = new List<LanguageLocalizationData>();

            foreach (ValueRange valueRange in ranges)
            {
                if(valueRange.Values == null)
                {
                    continue;
                }

                // Rows
                if(valueRange.Values.Count < 1)
                {
                    continue;
                }

                IList<object> columns = valueRange.Values[0];

                for (int column = localizationToolConfiguration.LanguagesStartingColumn; column < columns.Count; ++column)
                {
                    bool languageFound = TryGetLanguageAtColumn(
                        localizationToolConfiguration,
                        valueRange, 
                        column, 
                        out string language
                        );

                    if(!languageFound)
                    {
                        continue;
                    }

                    Dictionary<string, string> values = new Dictionary<string, string>();

                    for (int row = localizationToolConfiguration.ValuesAndTidsStartingRow; row < valueRange.Values.Count; ++row)
                    {
                        bool tidFound = TryGetTidAtRow(
                            localizationToolConfiguration,
                            valueRange, 
                            row, 
                            out string tid
                            );

                        if(!tidFound)
                        {
                            continue;
                        }

                        if(string.IsNullOrEmpty(tid))
                        {
                            continue;
                        }

                        bool valueFound = TryGetValueAtRow(valueRange, column, row, out string value);

                        if (!valueFound)
                        {
                            values.Add(tid, $"Missing {tid}");
                            continue;
                        }

                        bool alreadyAdded = values.ContainsKey(tid);

                        if(alreadyAdded)
                        {
                            UnityEngine.Debug.LogError($"Found duplicated tid {tid}");
                        }

                        values.Add(tid, value);
                    }

                    LanguageLocalizationData languageLocalizationData = new LanguageLocalizationData(
                        language,
                        values
                        );

                    languageValues.Add(languageLocalizationData);
                }
            }

            LocalizationData localizationData = new LocalizationData(languageValues);

            return TaskResult<LocalizationData>.FromResult(localizationData);
        }

        private static bool TryGetTidAtRow(
            LocalizationToolConfiguration localizationToolConfiguration,
            ValueRange rows, 
            int rowIndex, 
            out string tid
            )
        {
            if (rows.Values.Count <= rowIndex)
            {
                tid = string.Empty;
                return false;
            }

            IList<object> columns = rows.Values[rowIndex];

            if(columns.Count <= localizationToolConfiguration.TidsColumn)
            {
                tid = string.Empty;
                return false;
            }

            object tidObject = columns[localizationToolConfiguration.TidsColumn];

            if(tidObject == null)
            {
                tid = string.Empty;
                return false;
            }

            tid = tidObject.ToString();
            return true;
        }

        private static bool TryGetValueAtRow(ValueRange rows, int languageIndex, int rowIndex, out string value)
        {
            if (rows.Values.Count <= rowIndex)
            {
                value = string.Empty;
                return false;
            }

            IList<object> columns = rows.Values[rowIndex];

            if (columns.Count <= languageIndex)
            {
                value = string.Empty;
                return false;
            }

            object valueObject = columns[languageIndex];

            if (valueObject == null)
            {
                value = string.Empty;
                return false;
            }

            value = valueObject.ToString();
            return true;
        }

        private static bool TryGetLanguageAtColumn(
            LocalizationToolConfiguration localizationToolConfiguration,
            ValueRange valueRange, 
            int languageIndex, 
            out string value
            )
        {
            if (valueRange.Values.Count <= localizationToolConfiguration.LanguageNamesRow)
            {
                value = string.Empty;
                return false;
            }

            IList<object> columns = valueRange.Values[localizationToolConfiguration.LanguageNamesRow];

            if (columns.Count <= languageIndex)
            {
                value = string.Empty;
                return false;
            }

            object valueObject = columns[languageIndex];

            if (valueObject == null)
            {
                value = string.Empty;
                return false;
            }

            value = valueObject.ToString();
            return true;
        }
    }
}
