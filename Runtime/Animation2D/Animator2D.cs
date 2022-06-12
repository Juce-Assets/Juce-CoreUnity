using Juce.Core.Time;
using Juce.CoreUnity.Time;
using Juce.Extensions;
using System;
using UnityEngine;

namespace Juce.CoreUnity.Animation2D
{
    [RequireComponent(typeof(SpriteRenderer))]
    [ExecuteAlways]
    public sealed class Animator2D : MonoBehaviour
    {
        [SerializeField] [HideInInspector] private SpriteRenderer spriteRenderer = default;
        [SerializeField] [HideInInspector] private Animation2DPack animationPack = default;

        private Animation2D playingAnimation;
        private bool playinganimationNeedsToStart;
        private int playingAnimationSpriteIndex;

        public ITimer Timer { get; set; } = new ScaledUnityTimer();

        public bool FlipX
        {
            get { return spriteRenderer.flipX; }
            set { spriteRenderer.flipX = value; }
        }

        private void Awake()
        {
            TryGetSpriteRenderer();
        }

        private void Update()
        {
            TryGetSpriteRenderer();

            UpdatePlayingAnimation();
        }

        private void TryGetSpriteRenderer()
        {
            if (spriteRenderer != null)
            {
                return;
            }

            spriteRenderer = gameObject.GetOrAddComponent<SpriteRenderer>();
        }

        private bool TryGetAnimation(string name, out Animation2D animation2D)
        {
            if (animationPack == null)
            {
                animation2D = default;
                return false;
            }

            for (int i = 0; i < animationPack.Animations.Count; ++i)
            {
                Animation2D currAnimation = animationPack.Animations[i];

                if (string.Equals(currAnimation.Name, name))
                {
                    animation2D = currAnimation;
                    return true;
                }
            }

            animation2D = default;
            return false;
        }

        public void PlayAnimation(string animationName)
        {
            if (playingAnimation != null)
            {
                if (string.Equals(playingAnimation.Name, animationName))
                {
                    return;
                }
            }

            bool animationFound = TryGetAnimation(animationName, out Animation2D animaion);

            if (!animationFound)
            {
                return;
            }

            playingAnimation = animaion;
            playinganimationNeedsToStart = true;
        }

        private void UpdatePlayingAnimation()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            if (playingAnimation == null)
            {
                return;
            }

            if (playinganimationNeedsToStart)
            {
                playinganimationNeedsToStart = false;
                playingAnimationSpriteIndex = 0;

                Timer.Start();
            }

            if (Timer.Time.TotalSeconds > playingAnimation.PlaybackSpeed)
            {
                ++playingAnimationSpriteIndex;

                if (playingAnimationSpriteIndex >= playingAnimation.Sprites.Count)
                {
                    if (playingAnimation.Loop)
                    {
                        playingAnimationSpriteIndex = 0;
                    }
                    else
                    {
                        Timer.Reset();
                    }
                }

                Timer.Start();
            }

            if (playingAnimationSpriteIndex < playingAnimation.Sprites.Count)
            {
                spriteRenderer.sprite = playingAnimation.Sprites[playingAnimationSpriteIndex];
            }
        }
    }
}