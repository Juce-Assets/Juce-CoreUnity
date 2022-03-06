using Juce.Core.Refresh;
using Juce.Core.Visibility;
using System;
using UnityEngine;

namespace Juce.CoreUnity.ViewStack
{
    public class ViewStackEntry : IViewStackEntry
    {
        public Type Id { get; }
        public Transform Transform { get; }
        public IVisible Visible { get; }
        public IRefreshable ShowRefreshable { get; }
        public IRefreshable HideRefreshable { get; }
        public bool IsPopup { get; }

        public ViewStackEntry(
            Type id,
            Transform transform, 
            IVisible visible, 
            IRefreshable showRefreshable,
            IRefreshable hideRefreshable,
            bool isPopup
            )
        {
            Id = id;
            Transform = transform;
            Visible = visible;
            ShowRefreshable = showRefreshable;
            HideRefreshable = hideRefreshable;
            IsPopup = isPopup;
        }
    }
}
