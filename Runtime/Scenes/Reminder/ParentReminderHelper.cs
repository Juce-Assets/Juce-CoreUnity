using UnityEngine;
using UnityEngine.SceneManagement;

namespace Juce.CoreUnity.Scenes
{
    public static class ParentReminderHelper
    {
        public static void TraceBackAllInScene(Scene scene)
        {
            if(scene == null || !scene.IsValid())
            {
                return;
            }

            foreach (GameObject rootGameObjects in scene.GetRootGameObjects())
            {
                ParentReminderBreadcrumb[] breadcrumbs = rootGameObjects.GetComponentsInChildren<ParentReminderBreadcrumb>(
                    includeInactive: true
                    );

                foreach(ParentReminderBreadcrumb breadcrumb in breadcrumbs)
                {
                    breadcrumb.TraceBack();
                }
            }
        }
    }
}
