using Juce.Core.Refresh;
using Juce.Core.Visibility;
using System;
using UnityEngine;

namespace Juce.CoreUnity.ViewStack
{
    public interface IViewStackEntry 
    {
        Type Id { get; }
        Transform Transform { get; }
        IVisible Visible { get; }
        IRefreshable ShowRefreshable { get; }
        IRefreshable HideRefreshable { get; }
        bool IsPopup { get; }
    }
}
