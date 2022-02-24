﻿using Juce.CoreUnity.ViewStack.Sequences;

namespace Juce.CoreUnity.ViewStack
{
    public interface IUiViewStack
    {
        void Register(IViewStackEntry entry);
        void Unregister(IViewStackEntry entry);

        IViewStackSequenceBuilder New();
    }
}
