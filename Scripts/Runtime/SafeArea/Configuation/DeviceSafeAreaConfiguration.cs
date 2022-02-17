using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace Juce.CoreUnity.SafeArea
{
    /// <summary>
    /// Holds the settings of a SafeArea configuration for a list of devices
    /// </summary>
    [CreateAssetMenu(fileName = nameof(DeviceSafeAreaConfiguration), menuName = "Juce/SafeArea/" + nameof(DeviceSafeAreaConfiguration))]
    public class DeviceSafeAreaConfiguration : ScriptableObject
    {
        [Tooltip("Only used for debug and descriptive purposes")]
        [SerializeField] private string descriptiveName = default;

        [Tooltip("Using this values, you can intensify or decrease the offset applied by the safe area. " +
            "For example, setting a value to 0 will disable the safe area value on that specific side. Setting it " +
            "to 2 would multiply by 2 the offset applied by the safe area value")]
        [SerializeField] private SafeAreaData safeAreaData = default;

        public string DescriptiveName => descriptiveName;
        public SafeAreaData SafeAreaData => safeAreaData;
    }
}