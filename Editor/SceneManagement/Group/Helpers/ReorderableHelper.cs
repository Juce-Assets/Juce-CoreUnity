﻿using Juce.CoreUnity.SceneManagement.Group.Style;
using UnityEditor;
using UnityEngine;

namespace Juce.CoreUnity.SceneManagement.Group.Helpers
{
    public class ReorderableHelper
    {
        private int draggedStartId = -1;
        private int draggedEndId = -1;

        public void CheckDraggingItem(
            int index,
            Event ev,
            Rect interactionRect,
            Rect secondaryInteractionRect
            )
        {
            if (ev.type == EventType.MouseDown)
            {
                if (interactionRect.Contains(ev.mousePosition))
                {
                    draggedStartId = index;
                    ev.Use();
                }
            }

            // Draw rect if item is being dragged
            if (draggedStartId == index && interactionRect != Rect.zero)
            {
                EditorGUI.DrawRect(secondaryInteractionRect, SceneGroupEditorStyle.ReorderRectColor);
            }

            // If hovering at the top while dragging one, check where
            // it should be dropped: top or bottom
            bool rectContainsMousePosition = secondaryInteractionRect.Contains(ev.mousePosition);

            if (!rectContainsMousePosition || draggedStartId < 0)
            {
                return;
            }

            bool needsToChangeIndex = secondaryInteractionRect.Contains(ev.mousePosition);

            if (!needsToChangeIndex)
            {
                return;
            }

            draggedEndId = index;
        }

        public bool ResolveDragging(Event e, out int startIndex, out int endIndex)
        {
            bool ret = false;

            startIndex = -1;
            endIndex = -1;

            if (draggedStartId >= 0 && draggedEndId >= 0)
            {
                if (draggedEndId != draggedStartId)
                {
                    startIndex = draggedStartId;
                    endIndex = draggedEndId;

                    draggedStartId = draggedEndId;

                    ret = true;
                }
            }

            if (draggedStartId >= 0 || draggedEndId >= 0)
            {
                if (e.type == EventType.MouseUp)
                {
                    draggedStartId = -1;
                    draggedEndId = -1;
                    e.Use();
                }
            }

            return ret;
        }
    }
}