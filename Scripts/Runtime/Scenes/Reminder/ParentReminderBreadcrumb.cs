using UnityEngine;

namespace Juce.CoreUnity.Scenes
{
    public class ParentReminderBreadcrumb : MonoBehaviour
    {
        public ParentReminder ParentReminder { get; private set; }

        public void Init(ParentReminder parentReminder)
        {
            ParentReminder = parentReminder;
        }

        public void TraceBack()
        {
            if (ParentReminder == null)
            {
                UnityEngine.Debug.LogError($"Tried to trace back, but {nameof(ParentReminder)} was null, " +
                    $"at {nameof(ParentReminder)}", this);
                return;
            }

            ParentReminder.TraceBack();
        }
    }
}
