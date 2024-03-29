﻿using Juce.Core.Results;
using Juce.CoreUnity.Localization.Data;
using Juce.CoreUnity.Localization.Requests;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Localization.Services
{
    public class LocalizationService : ILocalizationService
    {
        private LocalizationData localizationData;
        private LanguageLocalizationData currentLanguageData;

        public int LanguagesCount { get; private set; }
        public int EntriesCount { get; private set; }

        public string CurrentLanguage { get; private set; } = string.Empty;

        public event Action OnLanguageChanged;

        public async Task Load(CancellationToken cancellationToken)
        {
            if(localizationData != null)
            {
                return;
            }

            ITaskResult<LocalizationData> result = await LoadLocalizationDataRequest.Execute();

            bool hasResult = result.TryGetResult(out localizationData);

            if (!hasResult)
            {
                UnityEngine.Debug.LogError("There was an error loading localization data");
                return;
            }

            if(localizationData.Values.Count == 0)
            {
                return;
            }

            LanguagesCount = localizationData.Values.Count;
            EntriesCount = localizationData.Values[0].Values.Count;

            string defaultLanguage = localizationData.Values[0].Language;

            SetCurrentLanguage(defaultLanguage);
        }

        public void SetCurrentLanguage(string currentLanguage)
        {
            if(localizationData == null)
            {
                return;
            }

            foreach(LanguageLocalizationData language in localizationData.Values)
            {
                bool isLanguage = string.Equals(language.Language, currentLanguage);

                if(!isLanguage)
                {
                    continue;
                }

                currentLanguageData = language;
                CurrentLanguage = language.Language;

                OnLanguageChanged?.Invoke();
            }
        }

        public string GetValue(string tid)
        {
            if(currentLanguageData == null)
            {
                return "No language found";
            }

            bool found = currentLanguageData.Values.TryGetValue(tid, out string value);

            if(!found)
            {
                return $"{tid} not found for {currentLanguageData.Language}";
            }

            return value;
        }
    }
}