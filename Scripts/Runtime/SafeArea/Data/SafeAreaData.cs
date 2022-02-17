using System;
using System.Collections.Generic;

using UnityEngine;

namespace Juce.CoreUnity.SafeArea
{
    /// <summary>
    /// Holds all the information for the configuration of a safe area
    /// </summary>
    [System.Serializable]
    public class SafeAreaData
    {
        public static readonly int UpIndex = 0;
        public static readonly int RightIndex = 1;
        public static readonly int DownIndex = 2;
        public static readonly int LeftIndex = 3;


        [SerializeField] private SafeAreaSideData up = default;
        [SerializeField] private SafeAreaSideData down = default;
        [SerializeField] private SafeAreaSideData left = default;
        [SerializeField] private SafeAreaSideData right = default;

        private readonly List<SafeAreaSideData> sidesData = new List<SafeAreaSideData>();

        /// <summary>
        /// Returns a list with all the data of the sides of the screen
        /// Every element can be accessed through index using the
        /// static index references on this class
        /// </summary>
        public IReadOnlyList<SafeAreaSideData> GetSidesData()
        {
            // We only need to fill this once
            if (sidesData.Count == 0)
            {
                sidesData.Add(up);
                sidesData.Add(right);
                sidesData.Add(down);
                sidesData.Add(left);
            }

            return sidesData;
        }
    }
}