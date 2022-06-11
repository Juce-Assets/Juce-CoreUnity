using System;
using System.Collections.Generic;

namespace Juce.CoreUnity.Ui.ViewStack.Context
{
    public interface IViewContext
    {
        Type ViewId { get; }
        List<Type> PopupsViewIds { get; }
    }
}
