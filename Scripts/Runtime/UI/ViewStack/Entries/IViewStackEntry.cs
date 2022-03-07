using Juce.Core.Refresh;
using Juce.Core.Visibility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.ViewStack.Entries
{
    public interface IViewStackEntry 
    {
        Type Id { get; }
        Transform Transform { get; }
        IVisible Visible { get; }
        IReadOnlyList<ViewStackEntryRefresh> RefreshList { get; }
        bool IsPopup { get; }
    }
}
