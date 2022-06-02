using Juce.CoreUnity.Service;
using Juce.CoreUnity.TextProcessors;
using Juce.Localization.Services;
using UnityEngine;

namespace Juce.CoreUnity.Localization.TextProcessorLayers
{
    public class LocalizationTextProcessorLayer : TextProcessorLayer
    {
        [SerializeField] private string format = "{0}";
        [SerializeField] private string tid = default;

        private readonly CachedService<ILocalizationService> localizationService = new CachedService<ILocalizationService>();

        private void Awake()
        {
            localizationService.Value.OnLanguageChanged += Refresh;
        }

        private void OnDestroy()
        {
            localizationService.Value.OnLanguageChanged -= Refresh;
        }

        public override string Process(string text)
        {
            string value = localizationService.Value.GetValue(tid);

            return string.Format(format, value); 
        }
    }
}