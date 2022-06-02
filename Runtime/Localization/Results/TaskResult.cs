namespace Juce.Loc.Results
{
	public struct TaskResult<T>
	{
		public bool HasResult { get; }
		public T Value { get; }

		private TaskResult(bool hasResult, T value)
        {
			HasResult = hasResult;
			Value = value;
		}

		public static TaskResult<T> FromResult(T value)
        {
			return new TaskResult<T>(true, value);
        }

		public static TaskResult<T> FromError()
		{
			return new TaskResult<T>(false, default);
		}
	}
}
