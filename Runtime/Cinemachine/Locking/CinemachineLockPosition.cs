#if JUCE_CINEMACHINE_EXTENSIONS

using UnityEngine;
using Cinemachine;

namespace Juce.CoreUnity.Cinemachine.Locking
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")] // Hide in menu
    public sealed class CinemachineLockPosition : CinemachineExtension
    {
        [Header("X Lock")]
        public bool XLockEnabled = false;
        public float XLockPosition = 0;

        [Header("Y Lock")]
        public bool YLockEnabled = false;
        public float YLockPosition = 0;

        [Header("Z Lock")]
        public bool ZLockEnabled = false;
        public float ZLockPosition = 0;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, 
            ref CameraState state, 
            float deltaTime
            )
        {
            if(stage != CinemachineCore.Stage.Body)
            {
                return;
            }

            Vector3 position = state.RawPosition;

            if (XLockEnabled)
            {
                position.x = XLockPosition;
            }

            if (YLockEnabled)
            {
                position.y = YLockPosition;
            }

            if (ZLockEnabled)
            {
                position.z = ZLockPosition;
            }

            state.RawPosition = position;
        }
    }
}

#endif