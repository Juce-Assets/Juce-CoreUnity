using Juce.Utils.Singletons;
using System.Collections.Generic;

namespace Juce.CoreUnity.Service
{
    public class ServicesProvider : AutoStartMonoSingleton<ServicesProvider>
    {
        private readonly List<IService> allServices = new List<IService>();
        private readonly List<IUpdatableService> updatableServices = new List<IUpdatableService>();

        private void Update()
        {
            UpdateUpdatableServices();
        }

        private void OnApplicationQuit()
        {
            CleanUpAllServices();
        }

        public static void Register<T>(T service) where T : IService
        {
            Instance.RegisterService(service);
        }

        public static void Unregister(IService service)
        {
            Instance.UnregisterService(service);
        }

        public void RegisterService<T>(T service) where T : IService
        {
            if (service == null)
            {
                throw new System.ArgumentNullException($"Tried to register null service at {nameof(ServicesProvider)}");
            }

            bool alreadyExists = TryGetService<T>(out _);

            if (alreadyExists)
            {
                throw new System.Exception($"Service {nameof(T)} has been already added at {nameof(ServicesProvider)}");
            }

            allServices.Add(service);

            IUpdatableService updatableService = service as IUpdatableService;

            if (updatableService != null)
            {
                updatableServices.Add(updatableService);
            }

            service.Init();
        }

        public void UnregisterService(IService service)
        {
            if (service == null)
            {
                throw new System.ArgumentNullException($"Tried to unregister null service at {nameof(ServicesProvider)}");
            }

            bool found = allServices.Remove(service);

            if (!found)
            {
                throw new System.Exception($"Tried to unregister service {service.GetType().Name} but it could" +
                    $"not be found at {nameof(ServicesProvider)}");
            }

            IUpdatableService updatableService = service as IUpdatableService;

            if (updatableService != null)
            {
                updatableServices.Remove(updatableService);
            }

            service.CleanUp();
        }

        public T GetService<T>() where T : IService
        {
            TryGetService(out T service);

            return service;
        }

        private bool TryGetService<T>(out T outService) where T : IService
        {
            for (int i = 0; i < allServices.Count; ++i)
            {
                if (allServices[i].GetType() == typeof(T))
                {
                    outService = (T)allServices[i];
                    return true;
                }
            }

            outService = default;
            return false;
        }

        private void UpdateUpdatableServices()
        {
            for (int i = 0; i < updatableServices.Count; ++i)
            {
                updatableServices[i].Update();
            }
        }

        private void CleanUpAllServices()
        {
            for (int i = 0; i < allServices.Count; ++i)
            {
                allServices[i].CleanUp();
            }

            updatableServices.Clear();
            allServices.Clear();
        }
    }
}