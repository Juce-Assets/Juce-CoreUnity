using Juce.CoreUnity.Localization.Constants;
using Juce.CoreUnity.Localization.Data;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;

namespace Juce.CoreUnity.Localization.Dawers
{
    public static class SaveLocalizationDataToDiskQuery
    {
        public static async Task<bool> Execute(LocalizationData localizationData, CancellationToken cancellationToken)
        {
            string path = ConstantEditorDirectories.OutputPath;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Formatting = Formatting.Indented;

            string dataString = JsonConvert.SerializeObject(localizationData, settings);

            byte[] dataBytes = Encoding.UTF8.GetBytes(dataString);

            try
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }
                else
                {
                    File.Delete(path);
                }

                using (FileStream sourceStream = File.Open(path, FileMode.OpenOrCreate))
                {
                    sourceStream.Seek(0, SeekOrigin.End);

                    await sourceStream.WriteAsync(dataBytes, 0, dataBytes.Length, cancellationToken);
                }
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError($"There was an exception trying to save bytes async to path " +
                    $"{path}, at {nameof(SaveLocalizationDataToDiskQuery)}");

                UnityEngine.Debug.LogException(exception);

                return false;
            }

            AssetDatabase.Refresh();

            return true;
        }
    }
}
