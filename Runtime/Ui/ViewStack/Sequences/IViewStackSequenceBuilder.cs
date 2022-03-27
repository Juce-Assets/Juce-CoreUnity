using System;
using System.Threading;
using System.Threading.Tasks;

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
        IViewStackSequenceBuilder MoveCurrentToForeground();
        IViewStackSequenceBuilder SetInteractable<T>(bool set);
        IViewStackSequenceBuilder CurrentSetInteractable(bool set);
        IViewStackSequenceBuilder Callback(Action callback);

        Task Execute(CancellationToken cancellationToken);
        void Execute();
    }
}
