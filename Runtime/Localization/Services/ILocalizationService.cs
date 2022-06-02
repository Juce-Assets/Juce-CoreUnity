using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Localization.Services
{
    public interface ILocalizationService
    {
        int LanguagesCount { get; }
        int EntriesCount { get; }

        string CurrentLanguage { get; }

        event Action OnLanguageChanged;

        Task Load(CancellationToken cancellationToken);
        void SetCurrentLanguage(string currentLanguage);
        string GetValue(string tid);
    }
}