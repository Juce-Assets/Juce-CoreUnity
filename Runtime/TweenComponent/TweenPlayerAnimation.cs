using Juce.CoreUnity.Ui;
using Juce.TweenComponent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.CoreUnity.TweenComponent
{
    public class TweenPlayerAnimation : TaskAnimationMonoBehaviour
    {
        [SerializeField] private List<TweenPlayer> play = default;
        [SerializeField] private List<TweenPlayer> kill = default;
        [SerializeField] private List<TweenPlayer> complete = default;

        public override Task Execute(bool instantly, CancellationToken cancellationToken)
        {
            foreach(TweenPlayer toKill in kill)
            {
                if(toKill == null)
                {
                    UnityEngine.Debug.LogError($"Null {nameof(TweenPlayer)} to kill at {nameof(TweenPlayerAnimation)} " +
                        $"{gameObject.name}", gameObject);
                    continue;
                }

                toKill.Kill();
            }

            foreach (TweenPlayer toComplete in complete)
            {
                if (toComplete == null)
                {
                    UnityEngine.Debug.LogError($"Null {nameof(TweenPlayer)} to complete at {nameof(TweenPlayerAnimation)} " +
                        $"{gameObject.name}", gameObject);
                    continue;
                }

                toComplete.Complete();
            }

            List<Task> toWait = new List<Task>(play.Count);

            foreach (TweenPlayer toPlay in play)
            {
                if (toPlay == null)
                {
                    UnityEngine.Debug.LogError($"Null {nameof(TweenPlayer)} to play at {nameof(TweenPlayerAnimation)} " +
                        $"{gameObject.name}", gameObject);
                    continue;
                }

                toWait.Add(toPlay.Play(instantly, cancellationToken));
            }

            return Task.WhenAll(toWait);
        }
    }
}
