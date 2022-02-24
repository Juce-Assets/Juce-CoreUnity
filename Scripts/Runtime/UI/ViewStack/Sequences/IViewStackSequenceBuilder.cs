namespace Juce.CoreUnity.ViewStack.Sequences
{
    public interface IViewStackSequenceBuilder
    {
        IViewStackSequenceBuilder Show<T>(bool instantly);
        IViewStackSequenceBuilder HideAndPush<T>(bool instantly);
        IViewStackSequenceBuilder Hide<T>(bool instantly);
        IViewStackSequenceBuilder ShowLast(bool instantly);
        IViewStackSequenceBuilder ShowLastBehindForeground(bool instantly);
        IViewStackSequenceBuilder MoveToBackground<T>();
    }
}
