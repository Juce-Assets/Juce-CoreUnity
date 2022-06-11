using Juce.Core.Results;
using Juce.CoreUnity.Files.Utils;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Persistence.Serialization
{
    public sealed class SerializableData<T> : ISerializableData<T> where T : class
    {
        private readonly string _localPath;
        private readonly JsonSerializerSettings _jsonSettings;

        private T _data;

        public event Action<T> OnSave;
        public event Action<T, bool> OnLoad;

        public T Data => GetData();

        public SerializableData(string localPath)
        {
            _localPath = localPath;

            _jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            TryGenerateData();

            OnSave?.Invoke(_data);

            try
            {
                string finalPath = SerializableDataUtils.GetPersistenceDataFilePathFromFileName(_localPath);

                string dataString = JsonConvert.SerializeObject(_data, _jsonSettings);
                byte[] dataBytes = Encoding.UTF8.GetBytes(dataString);

                await FilesUtils.SaveBytesAsync(finalPath, dataBytes, cancellationToken);

                UnityEngine.Debug.Log($"{nameof(SerializableData<T>)} {typeof(T).Name} saved \n {_data}");
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError($"Error saving {nameof(SerializableData<T>)} {typeof(T).Name} with " +
                    $"the following exception {exception}");
            }
        }

        public async Task Load(CancellationToken cancellationToken)
        {
            string finalPath = SerializableDataUtils.GetPersistenceDataFilePathFromFileName(_localPath);

            try
            {
                ITaskResult<byte[]> bytesResult = await FilesUtils.LoadBytesAsync(finalPath, cancellationToken);

                bool hasResult = bytesResult.TryGetResult(out byte[] bytes);

                if (!hasResult)
                {
                    TryGenerateData();

                    OnLoad?.Invoke(_data, /*First time:*/ true);

                    UnityEngine.Debug.Log($"{nameof(SerializableData<T>)} {typeof(T).Name} not found. Creating with default values");
                }
                else
                {
                    string result = Encoding.UTF8.GetString(bytes);

                    _data = JsonConvert.DeserializeObject<T>(result);

                    OnLoad?.Invoke(_data, /*First time:*/ false);

                    UnityEngine.Debug.Log($"{nameof(SerializableData<T>)} {typeof(T).Name} loaded \n {Data}");
                }

            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError($"Error loading {nameof(SerializableData<T>)} {typeof(T).Name} " +
                    $"with the following exception {exception}");
            }

            TryGenerateData();
        }

        T GetData()
        {
            if (_data == null)
            {
                TryGenerateData();

                UnityEngine.Debug.LogError($"Tried to get Data before it was loaded, at {nameof(SerializableData<T>)} {typeof(T).Name}");
            }

            return _data;
        }

        void TryGenerateData()
        {
            if (_data != null)
            {
                return;
            }

            _data = Activator.CreateInstance<T>();
        }
    }
}