using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Juce.CoreUnity.SafeArea
{
    /// <summary>
    /// Base settings of the SafeArea configuration
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ScreenSafeAreaConfiguration), menuName = "Juce/SafeArea/" + nameof(ScreenSafeAreaConfiguration))]
    public class ScreenSafeAreaConfiguration : ScriptableObject
    {
        [SerializeField] private ScreenOrientation referenceOrientation = ScreenOrientation.LandscapeLeft;
        [SerializeField] private SafeAreaData defaultData = default;
        [SerializeField] [HideInInspector] private List<DeviceSafeAreaData> devicesData = default;

        private string cachedDeviceName;
        private SafeAreaData cachedSafeAreaData;

        public List<DeviceSafeAreaData> DevicesData => devicesData;
        public ScreenOrientation ReferenceOrientation => referenceOrientation;

        // Returns the safe area configuration to use, checking if the
        // current device has specific settings that need to be used.
        // The value gets cached the first time is retrieved, and won't change
        // during execution
        public SafeAreaData GetSafeAreaDataToUse()
        {
            if (cachedSafeAreaData == null)
            {
                CacheSafeAreaDataToUse();
            }

            return cachedSafeAreaData;
        }

        private void CacheSafeAreaDataToUse()
        {
            string currDevice = SystemInfo.deviceModel;

            if(string.Equals(currDevice, SystemInfo.unsupportedIdentifier))
            {
                UnityEngine.Debug.Log($"Using default safe area for device: {currDevice}");
                cachedSafeAreaData = defaultData;
                return;
            }

            cachedDeviceName = currDevice;

            DeviceSafeAreaData deviceData = devicesData.FirstOrDefault(setting => setting.DeviceId == currDevice);

            if(deviceData == null || deviceData.Settings == null)
            {
                UnityEngine.Debug.Log($"Using default safe area for device: {currDevice}");
                cachedSafeAreaData = defaultData;
            }

            cachedSafeAreaData = deviceData.Settings.SafeAreaData;

            UnityEngine.Debug.Log($"Using custom safe area: {deviceData.Settings.DescriptiveName} " +
                $"for device: {currDevice}");
        }
    }
}