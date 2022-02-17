using Juce.Core.Time;
using System;
using UnityEngine;

namespace Juce.CoreUnity.Animation2D
{
    [RequireComponent(typeof(SpriteRenderer))]
    [ExecuteAlways]
    public class Animator2D : MonoBehaviour
    {
        [SerializeField] [HideInInspector] private SpriteRenderer spriteRenderer = default;
        [SerializeField] [HideInInspector] private Animation2DPack animationPack = default;

        private Animation2D playingAnimation;
        private bool playinganimationNeedsToStart;
        private int playingAnimationSpriteIndex;

        public ITimer Timer { get; set; }

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

        private void Init(Animation2DPack animationPack, ITimer timer)
        {
            if (animationPack == null)
            {
                throw new ArgumentNullException($"{nameof(Animation2DPack)} cannot be null on {nameof(Animator2D)}");
            }

            if (timer == null)
            {
                throw new ArgumentNullException($"{nameof(ITimer)} cannot be null on {nameof(Animator2D)}");
            }

            this.animationPack = animationPack;
            Timer = timer;
        }

        private void TryGetSpriteRenderer()
        {
            if (spriteRenderer != null)
            {
                return;
            }

            spriteRenderer = gameObject.GetOrAddComponent<SpriteRenderer>();
        }

        private Animation2D GetAnimation(string name)
        {
            if (animationPack == null)
            {
                throw new Exception($"{nameof(Animation2DPack)} cannot be null on {nameof(Animator2D)}");
            }

            for (int i = 0; i < animationPack.Animations.Count; ++i)
            {
                Animation2D currAnimation = animationPack.Animations[i];

                if (string.Equals(currAnimation.Name, name))
                {
                    return currAnimation;
                }
            }

            return null;
        }

        public void PlayAnimation(string name)
        {
            if (playingAnimation != null)
            {
                if (string.Equals(playingAnimation.Name, name))
                {
                    return;
                }
            }

            Animation2D animationToPlay = GetAnimation(name);

            if (animationToPlay == null)
            {
                return;
            }

            playingAnimation = animationToPlay;
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

            if (Timer == null)
            {
                throw new Exception($"{nameof(ITimer)} cannot be null on {nameof(Animator2D)}");
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