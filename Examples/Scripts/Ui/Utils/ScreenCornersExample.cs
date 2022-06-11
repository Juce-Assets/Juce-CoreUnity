using Juce.CoreUnity.Guizmo.Utils;
using Juce.CoreUnity.Ui.Utils;
using UnityEngine;

public class ScreenCornersExample : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform = default;
    [SerializeField] private RectTransform parentRectTransform = default;

    private void Start()
    {
        
    }

    private void Update()
    {
        Rect screenBoundsRectPosition = UiUtils.GetScreenBoundsRectPositionOverlayCamera(
            parentRectTransform
            );

        Rect substractedRect = UiUtils.SubstractRects(
            screenBoundsRectPosition,
            new Rect(rectTransform.anchoredPosition, rectTransform.sizeDelta)
            );

        UnityEngine.Debug.Log(
            $"Screen{screenBoundsRectPosition}, " +
            $"Rect{rectTransform.rect}, " +
            $"SubstractedRect{substractedRect.min} {substractedRect.max}");

        if(substractedRect.min.y < 0)
        {
            rectTransform.anchoredPosition = new Vector2(
                rectTransform.anchoredPosition.x,
                screenBoundsRectPosition.min.y
                );
        }

        if (substractedRect.max.y > 0)
        {
            rectTransform.anchoredPosition = new Vector2(
                rectTransform.anchoredPosition.x,
                screenBoundsRectPosition.max.y - rectTransform.sizeDelta.y
                );
        }

        if (substractedRect.min.x < 0)
        {
            rectTransform.anchoredPosition = new Vector2(
                screenBoundsRectPosition.min.x,
                rectTransform.anchoredPosition.y
                );
        }

        if (substractedRect.max.x > 0)
        {
            rectTransform.anchoredPosition = new Vector2(
                screenBoundsRectPosition.max.x - rectTransform.sizeDelta.x,
                rectTransform.anchoredPosition.y
                );
        }
    }

    private void OnDrawGizmos()
    {
        Rect bounds = UiUtils.GetRectTransformBoundsWorldPosition(rectTransform);

        GizmoUtils.DrawWireCube(bounds.center, bounds.size, Color.red);
    }
}
