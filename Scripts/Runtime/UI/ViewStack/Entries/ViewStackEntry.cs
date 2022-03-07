using Juce.Core.Visibility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.CoreUnity.ViewStack.Entries
{
    public class ViewStackEntry : IViewStackEntry
    {
        public Type Id { get; }
        public Transform Transform { get; }
        public IVisible Visible { get; }
        public IReadOnlyList<ViewStackEntryRefresh> RefreshList { get; }
        public bool IsPopup { get; }

        public ViewStackEntry(
            Type id,
            Transform transform, 
            IVisible visible,
            IReadOnlyList<ViewStackEntryRefresh> refreshList,
            bool isPopup
            )
        {
            Id = id;
            Transform = transform;
            Visible = visible;
            RefreshList = refreshList;
            IsPopup = isPopup;
        }
    }
}
