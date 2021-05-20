using System;
using System.Threading.Tasks;

public static class JuceCoreUnityTaskExtensions
{
    public static async void RunAsync(this Task task, Action onFinish = null)
    {
        await task;

        onFinish?.Invoke();
    }
}