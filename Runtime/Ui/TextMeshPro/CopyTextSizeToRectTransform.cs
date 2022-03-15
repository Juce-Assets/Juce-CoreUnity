using TMPro;
using UnityEngine;

namespace Juce.CoreUnity.Ui.Text
{
#if JUCE_TEXT_MESH_PRO_EXTENSIONS

    [ExecuteInEditMode]
    public class CopyTextSizeToRectTransform : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMPro.TextMeshProUGUI text = default;
        [SerializeField] private RectTransform rectTransform = default;

        [Header("Width")]
        [SerializeField] private bool width = true;
        [SerializeField] private float widthPadding = default;

        [Header("Height")]
        [SerializeField] private bool height = default;
        [SerializeField] private float heightPadding = default;

        private bool needsToUpdate;

        private void Awake()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnTextChanged);
        }

                private void OnDestroy()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(OnTextChanged);
        }

        private void OnEnable()
        {
            TryRefreshMesh();
            CopySize();
        }

        private void Update()
        {
            TryCopySize();
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
            if(!Application.isPlaying)
            {
                TryRefreshMesh();
                CopySize();
            }

            if (!needsToUpdate)
            {
                return;
            }

            needsToUpdate = false;

            CopySize();
        }

        private void TryRefreshMesh()
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (text == null)
            {
                return;
            }

            text.ForceMeshUpdate(true);
        }

        public void CopySize()
        {
            if(rectTransform == null)
            {
                return;
            }

            if(text == null)
            {
                return;
            }

            Vector2 newSize = rectTransform.sizeDelta;
            Vector2 textSize = text.GetRenderedValues();

            if (width)
            {
                textSize.x = Mathf.Max(0, textSize.x);
                newSize.x = textSize.x + widthPadding;
            }

            if (height)
            {
                textSize.y = Mathf.Max(0, textSize.y);
                newSize.y = textSize.y + heightPadding;
            }

            rectTransform.sizeDelta = newSize;
        }
    }

#endif
}
