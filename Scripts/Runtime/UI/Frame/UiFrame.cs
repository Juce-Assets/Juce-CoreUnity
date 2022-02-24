using System;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.Ui.Frame
{
    public class UiFrame : MonoBehaviour, IUiFrame
    {
        [SerializeField] private Canvas canvas = default;

        private readonly Dictionary<Transform, Transform> originalParents = new Dictionary<Transform, Transform>();

        public void Register(Transform transform)
        {
            originalParents.Add(transform, transform.parent);

            transform.SetParent(canvas.gameObject.transform, worldPositionStays: false);
            transform.SetAsFirstSibling();
        }

        public void Unregister(Transform transform)
        {
            bool found = originalParents.TryGetValue(transform, out Transform originalParent);

            if(!found)
            {
                return;
            }

            transform.SetParent(originalParent, worldPositionStays: false);
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
