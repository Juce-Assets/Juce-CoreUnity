using System;

using UnityEngine;

namespace Juce.CoreUnity.SafeArea
{
    /// <summary>
    /// Holds the information for one device id and its settings
    /// </summary>
    [System.Serializable]
    public class DeviceSafeAreaData
    {
        [SerializeField] private string deviceId = default;
        [SerializeField] private DeviceSafeAreaConfiguration settings = default;

        public string DeviceId
        {
            get { return deviceId; }
            set { deviceId = value; }
        }

        public DeviceSafeAreaConfiguration Settings
        {
            get { return settings; }
            set { settings = value; }
        }
    }
}