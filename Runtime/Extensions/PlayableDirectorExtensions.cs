﻿using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Playables;

namespace Juce.CoreUnity.Extensions
{
    public static class PlayableDirectorExtensions
    {
        public static Task Play(this PlayableDirector component, CancellationToken cancellationToken)
        {
            component.Play();

            return AwaitCompletition(component, cancellationToken);
        }

        public static Task Play(this PlayableDirector component, PlayableAsset playableAsset, CancellationToken cancellationToken)
        {
            component.Play(playableAsset);

            return AwaitCompletition(component, cancellationToken);
        }

        public static Task AwaitCompletition(this PlayableDirector component, CancellationToken cancellationToken)
        {
            if (component.state != PlayState.Playing)
            {
                return Task.CompletedTask;
            }

            TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();

            Action<PlayableDirector> onStoped = (PlayableDirector _) =>
            {
                taskCompletionSource.TrySetResult(default);
            };

            component.stopped += onStoped;

            cancellationToken.Register(() => onStoped?.Invoke(component));

            return taskCompletionSource.Task;
        }

        public static void Complete(this PlayableDirector component)
        {
            if (component.state != PlayState.Playing)
            {
                return;
            }

            component.playableGraph.GetRootPlayable(0).SetSpeed(double.MaxValue);
        }
    }
}
