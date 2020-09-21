using System;
using System.Threading.Tasks;

public static class JuceCoreTaskExtensions
{
    public static async void ExecuteAsync(this Task task, Action onFinish = null)
    {
        await task;

        onFinish?.Invoke();
    }
}

