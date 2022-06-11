using Juce.Core.Results;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Files.Utils
{
    public static class FilesUtils
    {
        public static async Task SaveBytesAsync(string filePath, byte[] data, CancellationToken cancellationToken)
        {
            try
            {
                bool fileAlreadyExists = File.Exists(filePath);

                if (!fileAlreadyExists)
                {
                    string filePathDirectory = Path.GetDirectoryName(filePath);

                    Directory.CreateDirectory(filePathDirectory);
                }
                else
                {
                    File.Delete(filePath);
                }

                using (FileStream sourceStream = File.Open(filePath, FileMode.OpenOrCreate))
                {
                    sourceStream.Seek(0, SeekOrigin.End);

                    await sourceStream.WriteAsync(data, 0, data.Length, cancellationToken);
                }
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError($"There was an exception trying to save bytes async to filePath " +
                    $"{filePath}, at {nameof(FilesUtils)}");

                UnityEngine.Debug.LogException(exception);
            }
        }

        public static async Task<ITaskResult<byte[]>> LoadBytesAsync(string path, CancellationToken cancellationToken)
        {
            bool fileExists = File.Exists(path);

            if (!fileExists)
            {
                return TaskResult<byte[]>.FromEmpty();
            }

            try
            {
                byte[] loadedBytes;

                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    loadedBytes = new byte[fileStream.Length];

                    await fileStream.ReadAsync(loadedBytes, 0, (int)fileStream.Length, cancellationToken);
                }

                return TaskResult<byte[]>.FromResult(loadedBytes);
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError($"There was an exception trying to load bytes async at path " +
                    $"{path}, at {nameof(FilesUtils)}");

                UnityEngine.Debug.LogException(exception);
            }

            return TaskResult<byte[]>.FromEmpty();
        }
    }
}