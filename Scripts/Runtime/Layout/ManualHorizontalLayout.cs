using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.Layout
{
    public class ManualHorizontalLayout : MonoBehaviour
    {
        [SerializeField] private ManualLayoutHorizontalAlignment alignment = default;
        [SerializeField] private float distanceBetweenElements = default;

        private RectTransform rectTransform;

        public Dictionary<RectTransform, Vector2> Calculate()
        {
            Dictionary<RectTransform, Vector2> ret = new Dictionary<RectTransform, Vector2>();

            TryGetRectTransform();

            if (rectTransform == null)
            {
                return ret;
            }

            int realIndex = 0;

            for (int i = 0; i < transform.childCount; ++i)
            {
                Transform child = transform.GetChild(i);

                RectTransform childRectTransform = child.GetComponent<RectTransform>();

                if (childRectTransform == null)
                {
                    continue;
                }

                float position = ManualLayoutCalculator.Calculate(
                    0,
                    transform.childCount,
                    realIndex,
                    distanceBetweenElements,
                    alignment
                    );

                ret.Add(childRectTransform, new Vector2(position, 0));

                ++realIndex;
            }

            return ret;
        }

        public void Refresh()
        {
            Dictionary<RectTransform, Vector2> result = Calculate();

            foreach(KeyValuePair<RectTransform, Vector2> item in result)
            {
                item.Key.anchoredPosition = item.Value;
            }
        }

        public void AddAndRefresh(Transform newTransform)
        {
            if(transform == null)
            {
                return;
            }

            newTransform.SetParent(transform, worldPositionStays: false);

            Refresh();
        }

        private void TryGetRectTransform()
        {
            if(gameObject == null)
            {
                return;
            }

            if(rectTransform != null)
            {
                return;
            }

            rectTransform = gameObject.GetComponent<RectTransform>();
        }
    }
}
