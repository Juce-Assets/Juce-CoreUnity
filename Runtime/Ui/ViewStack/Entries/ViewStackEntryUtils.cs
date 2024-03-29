﻿using Juce.CoreUnity.Extensions;
using Juce.CoreUnity.Ui.ViewStack.Enums;
using UnityEngine;

namespace Juce.CoreUnity.Ui.ViewStack.Entries
{
    public static class ViewStackEntryUtils 
    {
        public static void SetInteractable(IViewStackEntry entry, bool set)
        {
            CanvasGroup canvasGroup = entry.Transform.gameObject.GetOrAddComponent<CanvasGroup>();

            canvasGroup.interactable = set;
        }

        public static void Refresh(IViewStackEntry entry, RefreshType type)
        {
            foreach(ViewStackEntryRefresh refresh in entry.RefreshList)
            {
                if(refresh.RefreshType != type)
                {
                    continue;
                }

                refresh.Refreshable.Refresh();
            }
        }
    }
}
