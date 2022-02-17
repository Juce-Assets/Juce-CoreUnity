using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Files
{
    public static class FileUtils
    {
        public static async Task SaveBytesAsync(string path, byte[] data, CancellationToken cancellationToken)
        {
            try
            {
                if(!File.Exists(path))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }
                else
                {
                    File.Delete(path);
                }

                using (FileStream SourceStream = File.Open(path, FileMode.OpenOrCreate))
                {
                    SourceStream.Seek(0, SeekOrigin.End);

                    await SourceStream.WriteAsync(data, 0, data.Length, cancellationToken);
                }
            }
            catch(Exception exception)
            {
                UnityEngine.Debug.LogError($"There was an exception trying to save bytes async to path " +
                    $"{path}, at {nameof(FileUtils)}");

                UnityEngine.Debug.LogException(exception);
            }
        }

        public static async Task<byte[]> LoadBytesAsync(string path, CancellationToken cancellationToken)
        {
            bool fileDoesNotExist = !File.Exists(path);

            if(fileDoesNotExist)
            {
                return null;
            }

            byte[] loadedBytes;

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    loadedBytes = new byte[fileStream.Length];
                    await fileStream.ReadAsync(loadedBytes, 0, (int)fileStream.Length, cancellationToken);
                }

                return loadedBytes;
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError($"There was an exception trying to load bytes async at path " +
                    $"{path}, at {nameof(FileUtils)}");

                UnityEngine.Debug.LogException(exception);
            }

            return null;
        }
    }
}