using System;
using System.Collections.Generic;

namespace Juce.CoreUnity.Ui.ViewStack.Context
{
    public class ViewContext : IViewContext
    {
        public Type ViewId { get; }
        public List<Type> PopupsViewIds { get; } = new List<Type>();

        public ViewContext(Type viewId)
        {
            ViewId = viewId;
        }
    }
}
