using UnityEngine;

namespace Juce.CoreUnity.Ui.Utils
{
    public static class UiUtils
    {
        // Rect position
        // - Pivot to 0, 0 to get proper positions

        // Screen position:
        // - Top left 0, 0
        // - From 0, 0 to Screen.width, Screen.height

        // World position

        public static Rect GetScreenBoundsScreenPosition()
        {
            return new Rect(0, 0, Screen.width, Screen.height);
        }

        public static Rect GetScreenBoundsRectPositionOverlayCamera(
            RectTransform parentRectTransform
            )
        {
            Rect screenBoundsScreenPosition = GetScreenBoundsScreenPosition();

            Vector2 min = ScreenPositionToRectPositionOverlayCamera(
                screenBoundsScreenPosition.min,
                parentRectTransform
                );

            Vector2 max = ScreenPositionToRectPositionOverlayCamera(
                screenBoundsScreenPosition.max,
                parentRectTransform
                );

            return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        }

        public static Rect GetScreenBoundsRectPositionScreenCamera(
            RectTransform parentRectTransform,
            Camera screenCamera
            )
        {
            Rect screenBoundsScreenPosition = GetScreenBoundsScreenPosition();

            Vector2 min = ScreenPositionToRectPositionScreenCamera(
                screenBoundsScreenPosition.min,
                parentRectTransform,
                screenCamera
                );

            Vector2 max = ScreenPositionToRectPositionScreenCamera(
                screenBoundsScreenPosition.max,
                parentRectTransform,
                screenCamera
                );

            return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        }

        public static Rect RectTransformToScreenPosition(RectTransform transform)
        {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);

            Rect rect = new Rect(transform.position.x, Screen.height - transform.position.y, size.x, size.y);
            rect.x -= (transform.pivot.x * size.x);
            rect.y -= ((1.0f - transform.pivot.y) * size.y);

            return rect;
        }

        public static Vector2 ScreenPositionToRectPositionOverlayCamera(
            Vector2 screenPosition, 
            RectTransform parentRectTransform
            )
        {
            bool found = RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRectTransform,
                screenPosition,
                null,
                out Vector2 rectPosition
                );

            if(!found)
            {
                return Vector2.zero;
            }

            return rectPosition;
        }

        public static Vector2 ScreenPositionToRectPositionScreenCamera(
            Vector2 screenPosition,
            RectTransform parentRectTransform,
            Camera screenCamera
            )
        {
            bool found = RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRectTransform,
                screenPosition,
                screenCamera,
                out Vector2 rectPosition
                );

            if (!found)
            {
                return Vector2.zero;
            }

            return rectPosition;
        }

        public static Rect GetRectTransformBoundsWorldPosition(RectTransform rectTransform)
        {
            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);

            Bounds bounds = new Bounds(worldCorners[0], Vector3.zero);

            for (int i = 1; i < 4; ++i)
            {
                bounds.Encapsulate(worldCorners[i]);
            }

            return new Rect(bounds.min, bounds.size);
        }

        public static Rect SubstractRects(Rect rect1, Rect rect2)
        {
            return Rect.MinMaxRect(
                rect2.min.x - rect1.min.x,
                rect2.min.y - rect1.min.y,
                rect2.max.x - rect1.max.x,
                rect2.max.y - rect1.max.y
                );
        }
    }
}
