namespace Juce.CoreUnity
{
    public static class JuceAppliaction
    {
        public static bool IsDebug => UnityEngine.Application.isEditor || UnityEngine.Debug.isDebugBuild;

        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}
