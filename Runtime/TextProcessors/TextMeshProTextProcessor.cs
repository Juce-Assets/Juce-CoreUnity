using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.TextProcessors
{
    public class TextMeshProTextProcessor : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI text = default;
        [SerializeField] private List<TextProcessorLayer> layers = default;

        private string originalText;

        private void Awake()
        {
            foreach (TextProcessorLayer layer in layers)
            {
                layer.OnRefresh += Refresh;
            }

            originalText = text.text;
        }

        private void Start()
        {
            Refresh();
        }

        private void OnDestroy()
        {
            foreach (TextProcessorLayer layer in layers)
            {
                layer.OnRefresh -= Refresh;
            }
        }

        public void Refresh()
        {
            string currentText = originalText;

            foreach (TextProcessorLayer layer in layers)
            {
                currentText = layer.Process(currentText);
            }

            text.text = currentText;
        }
    }
}