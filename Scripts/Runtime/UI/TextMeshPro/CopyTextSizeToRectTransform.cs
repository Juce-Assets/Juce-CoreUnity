using TMPro;
using UnityEngine;

namespace Juce.CoreUnity.Ui.Text
{
#if JUCE_TEXT_MESH_PRO_EXTENSIONS

    public class CopyTextSizeToRectTransform : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMPro.TextMeshProUGUI text = default;
        [SerializeField] private RectTransform rectTransform = default;

        [Header("Width")]
        [SerializeField] private bool width = default;
        [SerializeField] private float widthPadding = default;

        [Header("Height")]
        [SerializeField] private bool height = default;
        [SerializeField] private float heightPadding = default;

        private bool needsToUpdate;

        private void Awake()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnTextChanged);
        }

        private void OnEnable()
        {
            CopySize();
        }

        private void Update()
        {
            TryCopySize();
        }

        private void OnDestroy()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(OnTextChanged);
        }

        private void OnTextChanged(Object obj)
        {
            if (obj.Equals(text) == false)
            {
                return;
            }

            needsToUpdate = true;
        }

        private void TryCopySize()
        {
            if (!needsToUpdate)
            {
                return;
            }

            needsToUpdate = false;

            CopySize();
        }

        public void CopySize()
        {
            Vector2 newSize = rectTransform.sizeDelta;

            if (width)
            {
                newSize.x = text.preferredWidth + widthPadding;
            }

            if (height)
            {
                newSize.y = text.preferredHeight + heightPadding;
            }

            rectTransform.sizeDelta = newSize;
        }
    }

#endif
}
