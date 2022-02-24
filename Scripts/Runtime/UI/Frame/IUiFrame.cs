using UnityEngine;

namespace Juce.CoreUnity.Ui.Frame
{
    public interface IUiFrame
    {
        void Register(Transform transform);
        void Unregister(Transform transform);
        void MoveToBackground(Transform transform);
        void MoveToForeground(Transform transform);
        void MoveBehindForeground(Transform transform);
    }
}
