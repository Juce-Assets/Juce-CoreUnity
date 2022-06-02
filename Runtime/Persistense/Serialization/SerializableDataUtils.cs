using System.IO;
using UnityEngine;

namespace Juce.Persistence.Serialization
{
    public static class SerializableDataUtils
    {
        public static string GetPersistenceDataFilepath(string fileName)
        {
            return string.Format(
                "{0}{1}",
                GetPersistenceDataFolder(),
                fileName
                );
        }

        public static string GetPersistenceDataFolder()
        {
            return Path.Combine(
                Application.persistentDataPath,
                string.Format("PersistenceData{0}", Path.DirectorySeparatorChar)
                );
        }

        public static void ClearPersistanceData()
        {
            string path = GetPersistenceDataFolder();

            Directory.Delete(path, recursive: true);
        }
    }
}