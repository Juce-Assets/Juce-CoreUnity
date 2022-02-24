using System;
using System.Collections.Generic;

namespace Juce.CoreUnity.ViewStack.Context
{
    public interface IViewContext
    {
        Type ViewId { get; }
        List<Type> PopupsViewIds { get; }
    }
}
