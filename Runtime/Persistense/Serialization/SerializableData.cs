using Juce.CoreUnity.Files;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.CoreUnity.Persistence.Serialization
{
    public class SerializableData<T> where T : class
    {
        private readonly string localPath;

        public event Action<T> OnSave;
        public event Action<T, bool> OnLoad;

        public T Data { get; private set; }

        public SerializableData(string localPath)
        {
            this.localPath = localPath;
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Formatting = Formatting.Indented;

            TryGenerateData();

            OnSave?.Invoke(Data);

            try
            {
                string finalPath = SerializableDataUtils.GetPersistenceDataFilepath(localPath);

                string dataString = JsonConvert.SerializeObject(Data, settings);
                byte[] dataBytes = Encoding.UTF8.GetBytes(dataString);

                await FileUtils.SaveBytesAsync(finalPath, dataBytes, cancellationToken);

                UnityEngine.Debug.Log($"{nameof(SerializableData<T>)} {typeof(T).Name} saved \n {Data}");
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError($"Error saving {nameof(SerializableData<T>)} {typeof(T).Name} with " +
                    $"the following exception {exception}");
            }
        }

        public async Task Load(CancellationToken cancellationToken)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Formatting = Formatting.Indented;

            string finalPath = SerializableDataUtils.GetPersistenceDataFilepath(localPath);

            try
            {
                byte[] bytes = await FileUtils.LoadBytesAsync(finalPath, cancellationToken);

                if (bytes != null)
                {
                    string result = Encoding.UTF8.GetString(bytes);

                    Data = JsonConvert.DeserializeObject<T>(result);

                    OnLoad?.Invoke(Data, false);

                    UnityEngine.Debug.Log($"{nameof(SerializableData<T>)} {typeof(T).Name} loaded \n {Data}");
                }
                else
                {
                    TryGenerateData();

                    OnLoad?.Invoke(Data, true);

                    UnityEngine.Debug.Log($"{nameof(SerializableData<T>)} {typeof(T).Name} not found. Creating with default values");
                }
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError($"Error loading {nameof(SerializableData<T>)} {typeof(T).Name} " +
                    $"with the following exception {exception}");
            }

            TryGenerateData();
        }

        private void TryGenerateData()
        {
            if (Data != null)
            {
                return;
            }

            Data = Activator.CreateInstance<T>();
        }
    }
}