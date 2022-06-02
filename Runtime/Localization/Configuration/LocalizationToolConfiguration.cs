using UnityEngine;

namespace Juce.Loc.Configuration
{
    [CreateAssetMenu(fileName = nameof(LocalizationToolConfiguration), 
        menuName = "Juce/Localization/" + nameof(LocalizationToolConfiguration), order = 1)]
    public class LocalizationToolConfiguration : ScriptableObject
    {
        [Header("Spreadsheet")]
        [SerializeField] private string spreadsheetKey = default;
        [SerializeField] private string credentialsJsonPath = default;

        [Header("Values")]
        [SerializeField, Min(0)] private int tidsColumn = 0;
        [SerializeField, Min(0)] private int languagesStartingColumn = 1;
        [SerializeField, Min(0)] private int languageNamesRow = 0;
        [SerializeField, Min(0)] private int valuesAndTidsStartingRow = 0;

        public string SpreadsheetKey => spreadsheetKey;
        public string CredentialsJsonPath => credentialsJsonPath;

        public int TidsColumn => tidsColumn;
        public int LanguagesStartingColumn => languagesStartingColumn;
        public int LanguageNamesRow => languageNamesRow;
        public int ValuesAndTidsStartingRow => valuesAndTidsStartingRow;
    }
}
