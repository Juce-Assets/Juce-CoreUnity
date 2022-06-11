using System.IO;
using UnityEngine;

namespace Juce.CoreUnity.Persistence.Serialization
{
    public static class SerializableDataUtils
    {
        public static string GetPersistenceDataFilePathFromFileName(string fileName)
        {
            return string.Format(
                "{0}{1}",
                GetSerializableDataDirectory(),
                fileName
                );
        }

        public static string GetSerializableDataDirectory()
        {
            return Path.Combine(
                Application.persistentDataPath,
                string.Format("SerializableData{0}", Path.DirectorySeparatorChar)
                );
        }

        public static void ClearAllSerializableData()
        {
            string path = GetSerializableDataDirectory();

            Directory.Delete(path, recursive: true);
        }
    }
}