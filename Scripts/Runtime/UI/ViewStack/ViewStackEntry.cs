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
        public IRefreshable Refreshable { get; }
        public bool IsPopup { get; }

        public ViewStackEntry(
            Type id,
            Transform transform, 
            IVisible visible, 
            IRefreshable refreshable,
            bool isPopup
            )
        {
            Id = id;
            Transform = transform;
            Visible = visible;
            Refreshable = refreshable;
            IsPopup = isPopup;
        }
    }
}
