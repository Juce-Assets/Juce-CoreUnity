using Juce.CoreUnity.Contracts;
using Juce.Utils.Singletons;
using System;
using UnityEngine;

namespace Juce.CoreUnity.UI
{
    public class UIFrame : MonoSingleton<UIFrame>
    {
        [SerializeField] private Canvas canvas = default;

        private void Awake()
        {
            Contract.IsNotNull(canvas, this);

            InitInstance(this);
        }

        public void Register(UIView uiView)
        {
            uiView.transform.SetParent(canvas.gameObject.transform, worldPositionStays: false);
            uiView.transform.SetAsFirstSibling();
        }

        public void MoveBack(UIView uiView)
        {
            uiView.transform.SetParent(canvas.gameObject.transform, worldPositionStays: false);
            uiView.transform.SetAsFirstSibling();
        }

        public void PushForeground(UIView uiView)
        {
            uiView.transform.SetParent(canvas.gameObject.transform, worldPositionStays: false);
            uiView.transform.SetAsLastSibling();
        }

        public void PushBehindForeground(UIView uiView)
        {
            int index = canvas.gameObject.transform.childCount - 2;

            index = Math.Max(index, 0);

            uiView.transform.SetParent(canvas.gameObject.transform, worldPositionStays: false);
            uiView.transform.SetSiblingIndex(index);
        }
    }
}
