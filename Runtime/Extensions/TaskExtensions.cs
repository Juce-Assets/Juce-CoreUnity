using Juce.Core.Extensions;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.CoreUnity.Extensions
{
    public static class TaskExtensions
    {
        public static IEnumerator ToCoroutine(this Task task)
        {
            task.RunAsync();

            yield return new WaitUntil(() => task.IsCompleted);
        }

        public static IEnumerator ToCoroutine(this Task task, Action<Exception> onException)
        {
            task.RunAsync(onException);

            yield return new WaitUntil(() => task.IsCompleted);
        }
    }
}
