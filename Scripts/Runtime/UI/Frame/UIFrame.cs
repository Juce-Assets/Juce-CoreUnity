using System;
using UnityEngine;

namespace Juce.CoreUnity.Ui.Frame
{
    public class UiFrame : MonoBehaviour, IUiFrame
    {
        [SerializeField] private Canvas canvas = default;

        public void Register(Transform transform)
        {
            transform.SetParent(canvas.gameObject.transform, worldPositionStays: false);
            transform.SetAsFirstSibling();
        }

        public void MoveToBackground(Transform transform)
        {
            transform.SetParent(canvas.gameObject.transform, worldPositionStays: false);
            transform.SetAsFirstSibling();
        }

        public void MoveToForeground(Transform transform)
        {
            transform.SetParent(canvas.gameObject.transform, worldPositionStays: false);
            transform.SetAsLastSibling();
        }

        public void MoveBehindForeground(Transform transform)
        {
            int index = canvas.gameObject.transform.childCount - 2;

            index = Math.Max(index, 0);

            transform.SetParent(canvas.gameObject.transform, worldPositionStays: false);
            transform.SetSiblingIndex(index);
        }
    }
}
