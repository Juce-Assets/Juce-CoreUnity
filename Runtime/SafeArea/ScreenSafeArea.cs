using System.Collections.Generic;

using UnityEngine;

namespace Juce.CoreUnity.SafeArea
{
    /// <summary>
    /// This component should be placed on the RectTransform that needs to be affected by the SafeArea settings.
    /// </summary>
    public class ScreenSafeArea : MonoBehaviour
    {
        [SerializeField] private ScreenSafeAreaConfiguration settings = default;

        private RectTransform rectTransform = default;

        private bool prevExecuteInEditMode = false;

        private SafeAreaData safeAreaData;
        private IReadOnlyList<SafeAreaSideData> sidesData;

        private ScreenOrientation lastOrientation;

        private SafeAreaSideData leftSideData;
        private SafeAreaSideData rightSideData;
        private SafeAreaSideData upSideData;
        private SafeAreaSideData downSideData;

        private Rect lastSafeArea;

        protected virtual void Reset()
        {
            rectTransform = transform as RectTransform;
        }

        private void Awake()
        {
            TryGetRectTransform();

            UpdateOrientation(force: true);

            UpdateRectTransform(force: true);
        }

        private void Update()
        {
            UpdateOrientation();

            UpdateRectTransform(force: false);
        }

        private void CheckSettings()
        {
            if(settings != null)
            {
                return;
            }

            UnityEngine.Debug.LogError($"{nameof(ScreenSafeAreaConfiguration)} is null on " +
                $"{gameObject.name}. Screen safe area won't work, at {nameof(ScreenSafeArea)}", gameObject);
        }

        private void TryGetRectTransform()
        {
            if (rectTransform != null)
            {
                return;
            }

            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        /// <summary>
        /// Checks if the orientation has changed (except when forced == true), and updates the
        /// safe area depending on the new orientation.
        /// </summary>
        private void UpdateOrientation(bool force = false)
        {
            if (settings == null)
            {
                return;
            }

            ScreenOrientation currOrientation = Screen.orientation;

            bool shouldUpdate = force || lastOrientation != currOrientation;

            if (!shouldUpdate)
            {
                return;
            }

            UnityEngine.Debug.Log($"Orientation changed to {currOrientation}");

            lastOrientation = currOrientation;

            // Gets the safe area settings that we are going to use. It could be the
            // default settings, or settings for a specific device
            safeAreaData = settings.GetSafeAreaDataToUse();

            // From the settings, we get the 4 sides of the screen values that we are going
            // to use to calculate the final safe area
            sidesData = safeAreaData.GetSidesData();

            // Using the current orientation and on the reference orientation, it gets the actual
            // values for each side of the screen, so they match the ones that were set on the reference
            leftSideData = GetSideData(currOrientation, SafeAreaData.LeftIndex);
            rightSideData = GetSideData(currOrientation, SafeAreaData.RightIndex);
            upSideData = GetSideData(currOrientation, SafeAreaData.UpIndex);
            downSideData = GetSideData(currOrientation, SafeAreaData.DownIndex);
        }

        /// <summary>
        /// Gets the safe area defined by Unity, with our settings applied to it
        /// </summary>
        private bool TryGetSafeAreaRect(out Rect rect)
        {
            if (settings == null)
            {
                rect = default;
                return false;
            }

            rect = new Rect();

            // We transform to absolute coordinates here
            Rect safeArea = Screen.safeArea;
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;

            rect.x = safeArea.position.x;
            rect.y = safeArea.position.y;
            rect.width = safeArea.position.x + safeArea.size.x;
            rect.height = safeArea.position.y + safeArea.size.y;

            //Apply side data
            if (leftSideData != null)
            {
                rect.x = leftSideData.Use ? rect.x * leftSideData.Multiplier : 0;
            }

            if (downSideData != null)
            {
                rect.y = downSideData.Use ? rect.y * downSideData.Multiplier : 0;
            }

            if (rightSideData != null)
            {
                rect.width = rightSideData.Use ? screenWidth + ((rect.width - screenWidth) * rightSideData.Multiplier) : screenWidth;
            }

            if (upSideData != null)
            {
                rect.height = upSideData.Use ? screenHeight + ((rect.height - screenHeight) * upSideData.Multiplier) : screenHeight;
            }

            return true;
        }

        /// <summary>
        /// Updates the RecTransform using the SafeArea values.
        /// Based on Unity's implementation: https://connect.unity.com/p/updating-your-gui-for-the-iphone-x-and-other-notched-devices
        /// </summary>
        private void UpdateRectTransform(bool force = false)
        {
            if(rectTransform == null)
            {
                return;
            }

            bool couldGetSafeAreaRect = TryGetSafeAreaRect(out Rect safeAreaRect);

            if(!couldGetSafeAreaRect)
            {
                return;
            }

            bool needsToUpdate = force || safeAreaRect != lastSafeArea;

            if (!needsToUpdate)
            {
                return;
            }

            UnityEngine.Debug.Log("Updating safe area rect transform");

            lastSafeArea = safeAreaRect;

            safeAreaRect.x /= Screen.width;
            safeAreaRect.y /= Screen.height;
            safeAreaRect.width /= Screen.width;
            safeAreaRect.height /= Screen.height;

            Vector2 anchorMin = new Vector2(safeAreaRect.x, safeAreaRect.y);
            Vector2 anchorMax = new Vector2(safeAreaRect.width, safeAreaRect.height);

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }

        /// <summary>
        /// We return the side that matches the current rotation, using the reference orientation.
        /// With the current sideIndex, and the number of rotations applied to the phone,
        /// we can calculate the matching side data that we want to use
        /// </summary>

        // Reference orientation                        New orientation
        //           A
        //
        //    +-------------+
        //    |             |
        //    |             |
        //    |             |
        //    |             |                                   D
        //    |             |
        //    |             |                +------------------------------------+
        //    |             |                |                                    |
        //    |             |    90º right   |                                    |
        //  D |             | B  +------>  C |                                    |
        //    |             |                |                                    | A
        //    |             |                |                                    |
        //    |             |                |                                    |
        //    |             |                +------------------------------------+
        //    |             |
        //    |             |                                  B
        //    |             |
        //    |             |
        //    |             |
        //    +-------------+
        //  
        //           C

        private SafeAreaSideData GetSideData(ScreenOrientation orientation, int sideIndex)
        {
            if (settings == null)
            {
                return null;
            }

            int indexDifference = SafeAreaUtils.GetReferenceOrientationDiff(settings, orientation);

            int finalIndex = sideIndex + indexDifference;

            // Make sure value is always between, and including, [0, 3]
            finalIndex %= 4;

            return sidesData[finalIndex];
        }
    }
}