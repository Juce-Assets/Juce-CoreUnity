using UnityEngine;

namespace Juce.Extensions
{
    public static class ColorExtensions
    {
        public static bool IsHtmlColor(this Color color, string htmlColor)
        {
            string pixelHtmlColor = ColorUtility.ToHtmlStringRGB(color);

            string FormatHtmlColor(string htmlColorString)
            {
                if (string.IsNullOrEmpty(htmlColorString))
                {
                    return htmlColorString;
                }

                return htmlColorString.Replace("#", "").ToLower();
            }

            return string.Equals(FormatHtmlColor(pixelHtmlColor), FormatHtmlColor(htmlColor));
        }
    }
}