using System.Collections.Generic;

namespace Juce.Loc.Data
{
    public class LanguageLocalizationData 
    {
        public string Language { get; }
        public IReadOnlyDictionary<string, string> Values { get; }

        public LanguageLocalizationData(
            string language,
            IReadOnlyDictionary<string, string> values
            )
        {
            Language = language;
            Values = values;
        }
    }
}
