using Juce.TweenComponent;
using UnityEngine;

namespace Juce.CoreUnity.TweenComponent
{
    public class PlayTweenPlayerInstantlyOnAwake : MonoBehaviour
    {
        [SerializeField] private TweenPlayer tweenPlayer = default;

        private void Awake()
        {
            tweenPlayer.Play(instantly: true);
        }
    }
}
