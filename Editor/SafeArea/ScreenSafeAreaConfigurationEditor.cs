using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Juce.CoreUnity.SafeArea
{
    [CustomEditor(typeof(ScreenSafeAreaConfiguration))]
    public class ScreenSafeAreaConfigurationEditor : Editor
    {
        private Vector2 scrollViewPos = default;

        private readonly List<DeviceSafeAreaData> toRemove = new List<DeviceSafeAreaData>();

        public ScreenSafeAreaConfiguration ActualTarget { get; private set; }

        private void OnEnable()
        {
            ActualTarget = (ScreenSafeAreaConfiguration)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginScrollView(scrollViewPos);
            {
                DrawDefaultInspector();

                EditorGUILayout.Separator();
                EditorGUILayout.Space();

                DrawDevicesGUI();
                AddDeviceGUI();
            }
            EditorGUILayout.EndScrollView();
        }

        private void DrawDevicesGUI()
        {
            EditorGUILayout.LabelField("Custom devices configuration");

            EditorGUILayout.HelpBox("Exact format of model name (operating system dependent), for example iOS device names typically " +
                "look like 'iPhone6, 1', whereas Android device names are often in 'manufacturer model' format " +
                "(e.g. 'LGE Nexus 5' or 'SAMSUNG - SM - G900A'). The returned model can also be a very generic name (e.g. 'PC') if the " +
                "actual model name is not available or relevant on the current platform. The returned value will usually be similar " +
                "to the one shown in the operating system's 'About Device' or 'System Information' screen (or equivalent). " +
                "This information will often also include the manufacturer.", MessageType.Info);

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Model", GUILayout.MaxWidth(150));
            }
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < ActualTarget.DevicesData.Count; ++i)
            {
                DeviceSafeAreaData currDevice = ActualTarget.DevicesData[i];

                EditorGUILayout.BeginHorizontal();
                {
                    string currDeviceId = currDevice.DeviceId;
                    currDevice.DeviceId = GUILayout.TextArea(currDeviceId, GUILayout.MaxWidth(150));

                    if (currDevice.DeviceId != currDeviceId)
                    {
                        EditorUtility.SetDirty(this);
                    }

                    DeviceSafeAreaConfiguration currSettings = currDevice.Settings;

                    currDevice.Settings = EditorGUILayout.ObjectField(currSettings, typeof(DeviceSafeAreaConfiguration), true)
                                          as DeviceSafeAreaConfiguration;

                    if (currDevice.Settings != currSettings)
                    {
                        EditorUtility.SetDirty(this);
                    }

                    if (GUILayout.Button(" ▲", GUILayout.MaxWidth(23)))
                    {
                        if (i - 1 >= 0)
                        {
                            DeviceSafeAreaData toSwap = ActualTarget.DevicesData[i - 1];
                            ActualTarget.DevicesData[i - 1] = ActualTarget.DevicesData[i];
                            ActualTarget.DevicesData[i] = toSwap;

                            EditorUtility.SetDirty(this);
                        }
                    }

                    if (GUILayout.Button(" ▼", GUILayout.MaxWidth(23)))
                    {
                        if (i + 1 < ActualTarget.DevicesData.Count)
                        {
                            DeviceSafeAreaData toSwap = ActualTarget.DevicesData[i + 1];
                            ActualTarget.DevicesData[i + 1] = ActualTarget.DevicesData[i];
                            ActualTarget.DevicesData[i] = toSwap;

                            EditorUtility.SetDirty(this);
                        }
                    }

                    if (GUILayout.Button("X", GUILayout.MaxWidth(40)))
                    {
                        toRemove.Add(currDevice);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            if (toRemove.Count > 0)
            {
                for (int i = 0; i < toRemove.Count; ++i)
                {
                    ActualTarget.DevicesData.Remove(toRemove[i]);
                }

                toRemove.Clear();

                EditorUtility.SetDirty(this);
            }
        }

        private void AddDeviceGUI()
        {
            if (GUILayout.Button("Add device"))
            {
                DeviceSafeAreaData data = new DeviceSafeAreaData();

                ActualTarget.DevicesData.Add(data);

                EditorUtility.SetDirty(this);
            }
        }
    }
}