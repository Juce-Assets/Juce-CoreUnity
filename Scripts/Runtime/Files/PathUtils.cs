using System.IO;
using UnityEngine;

namespace Juce.CoreUnity.Files
{
    public static class PathUtils
    {
        public static string PersistentDataPath => Application.persistentDataPath;
        public static char DirectorySeparatorChar => Path.DirectorySeparatorChar;

        public static string CombinePaths(params string[] paths)
        {
            return Path.Combine(paths);
        }
    }
}