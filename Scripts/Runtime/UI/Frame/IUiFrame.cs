using UnityEngine;

namespace Juce.CoreUnity.Ui.Frame
{
    public interface IUiFrame
    {
        void Register(Transform transformProvider);
        void MoveToBackground(Transform transformProvider);
        void MoveToForeground(Transform transformProvider);
        void MoveBehindForeground(Transform transformProvider);
    }
}
