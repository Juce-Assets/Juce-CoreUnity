using UnityEngine;

namespace Juce.CoreUnity.Contracts
{
    public static class Contract
    {
        public static void IsNotNull(Object obj, Object owner)
        {
            if(obj != null)
            {
                return;
            }

            UnityEngine.Debug.LogError($"Null reference at {owner.GetType().Name}", owner);
        }
    }
}
