using UnityEngine;

namespace Juce.Extensions
{
    public static class CameraExtensions
    {
        public static float GetLeftSideWorldPosition(this Camera cam)
        {
            return cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x;
        }

        public static float GetRightSideWorldPosition(this Camera cam)
        {
            return cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, cam.nearClipPlane)).x;
        }

        public static float GetBottomSideWorldPosition(this Camera cam)
        {
            return cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).y;
        }

        public static float GetTopSideWorldPosition(this Camera cam)
        {
            return cam.ScreenToWorldPoint(new Vector3(0, Screen.height, cam.nearClipPlane)).y;
        }

        public static Rect GetWorldPositionRect(this Camera cam)
        {
            Vector2 min = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector2 max = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));

            return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        }

        public static float GetFrustumHeightAtCameraDistance(this Camera camera, float cameraDistance)
        {
            return 2.0f * cameraDistance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        public static float GetFrustumWidthAtCameraDistance(this Camera camera, float cameraDistance)
        {
            float horizontalFieldOfView = Camera.VerticalToHorizontalFieldOfView(camera.fieldOfView, camera.aspect);

            return 2.0f * cameraDistance * Mathf.Tan(horizontalFieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        public static Vector2 GetFrustumSizeAtCameraDistance(this Camera camera, float cameraDistance)
        {
            float width = camera.GetFrustumWidthAtCameraDistance(cameraDistance);
            float height = camera.GetFrustumHeightAtCameraDistance(cameraDistance);

            return new Vector2(width, height);
        }

        public static float GetCameraDistanceAtFrustumHeight(this Camera camera, float frustumHeight)
        {
            return frustumHeight * 0.5f / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        public static float GetCameraDistanceAtFrustumWidth(this Camera camera, float frustumWidth)
        {
            float horizontalFieldOfView = Camera.VerticalToHorizontalFieldOfView(camera.fieldOfView, camera.aspect);

            return frustumWidth * 0.5f / Mathf.Tan(horizontalFieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        public static Vector2 GetCameraDistanceAtFrustumSize(this Camera camera, Vector2 frustumSize)
        {
            float width = camera.GetCameraDistanceAtFrustumWidth(frustumSize.x);
            float height = camera.GetCameraDistanceAtFrustumHeight(frustumSize.y);

            return new Vector2(width, height);
        }
    }
}