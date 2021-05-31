using UnityEngine;

namespace Juce.CoreUnity.Scenes
{
    public class ParentReminder : MonoBehaviour
    {
        private GameObject breadcrumb;

        private void Awake()
        {
            Register();
        }

        private void Register()
        {
            breadcrumb = new GameObject($"{gameObject.name}-Breadcrumb");
            breadcrumb.transform.SetParent(gameObject.transform.parent, worldPositionStays: false);
            breadcrumb.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex());

            ParentReminderBreadcrumb parentReminderBreadcrumb = breadcrumb.AddComponent<ParentReminderBreadcrumb>();
            parentReminderBreadcrumb.Init(this);
        }

        public void TraceBack()
        {
            if(breadcrumb == null)
            {
                UnityEngine.Debug.LogError($"Tried to trace back, but breadcrumb was null, at {nameof(ParentReminder)}", this);
                return;
            }

            gameObject.transform.SetParent(breadcrumb.transform.parent);
            gameObject.transform.SetSiblingIndex(breadcrumb.transform.GetSiblingIndex());
        }
    }
}
