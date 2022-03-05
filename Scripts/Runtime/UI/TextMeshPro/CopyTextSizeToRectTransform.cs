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

        [Header("Values")]
        [SerializeField] private bool width = default;
        [SerializeField] private bool height = default;

        private void Awake()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnTextChanged);
        }

        private void OnEnable()
        {
            CopySize();
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

            CopySize();
        }

        private void CopySize()
        {
            Vector2 newSize = rectTransform.sizeDelta;

            if (width)
            {
                newSize.x = text.preferredWidth;
            }

            if (height)
            {
                newSize.y = text.preferredHeight;
            }

            rectTransform.sizeDelta = newSize;
        }
    }

#endif
}
