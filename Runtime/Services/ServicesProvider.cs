using System;
using System.Collections.Generic;
using Juce.Utils.Contracts;
using Juce.Utils.Singletons;

namespace Juce.Core.Services
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
            Contract.IsNotNull(service, "Trying to register null service");

            bool alreadyExists = TryGetService<T>(out _);

            Contract.IsFalse(alreadyExists, $"Service {nameof(T)} has been already added");

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
            Contract.IsNotNull(service, "Trying to unregister null service");

            bool found = allServices.Remove(service);

            IUpdatableService updatableService = service as IUpdatableService;

            if (updatableService != null)
            {
                updatableServices.Remove(updatableService);
            }

            Contract.IsTrue(found, $"Trying  to unregister service {service.GetType().Name} but it was not registered");

            service.CleanUp();
        }

        public T GetService<T>() where T : IService
        {
            T service;

            bool found = TryGetService<T>(out service);

            Contract.IsTrue(found, $"Service {nameof(T)} could not be found");

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
