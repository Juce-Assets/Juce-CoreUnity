using Juce.CoreUnity.Contracts;
using Juce.Utils.Singletons;
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

        public void Push(UIView uiView)
        {
            uiView.transform.SetParent(canvas.gameObject.transform, worldPositionStays: false);
            uiView.transform.SetAsLastSibling();
        }
    }
}
