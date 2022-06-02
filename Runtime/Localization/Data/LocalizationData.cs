using System.Collections.Generic;

namespace Juce.Localization.Data
{
    public class LocalizationData 
    {
        public IReadOnlyList<LanguageLocalizationData> Values { get; }

        public LocalizationData(IReadOnlyList<LanguageLocalizationData> values)
        {
            Values = values;
        }
    }
}
