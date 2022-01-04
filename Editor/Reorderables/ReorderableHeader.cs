using System;

using UnityEditor;

using UnityEngine;

namespace Juce.CoreUnity.Reorderables
{
    public static class ReorderableHeader
    {
        public static void Draw(
            string title,
            string extraTitle,
            Action showGenericMenu,
            out Rect reorderInteractionRect,
            out Rect secondaryInteractionRect
            )
        {
            bool enabled = true;
            bool folded = false;

            Draw(
                title,
                extraTitle,
                showGenericMenu,
                canBeDisabled: false,
                canBeFolded: false,
                ref enabled,
                ref folded,
                out reorderInteractionRect,
                out secondaryInteractionRect
                );
        }

        public static void Draw(
            string title,
            string extraTitle,
            ref bool folded,
            Action showGenericMenu,
            out Rect reorderInteractionRect,
            out Rect secondaryInteractionRect
            )
        {
            bool enabled = true;

            Draw(
                title,
                extraTitle,
                showGenericMenu,
                canBeDisabled: false,
                canBeFolded: true,
                ref enabled,
                ref folded,
                out reorderInteractionRect,
                out secondaryInteractionRect
                );
        }

        public static void Draw(
            string title,
            string extraTitle,
            Action showGenericMenu,
            ref bool enabled,
            ref bool folded,
            out Rect reorderInteractionRect,
            out Rect secondaryInteractionRect
            )
        {
            Draw(
                title,
                extraTitle,
                showGenericMenu,
                canBeDisabled: true,
                canBeFolded: true,
                ref enabled,
                ref folded,
                out reorderInteractionRect,
                out secondaryInteractionRect
                );
        }

        public static void Draw(
            string title,
            string extraTitle,
            Action showGenericMenu,
            bool canBeDisabled,
            bool canBeFolded,
            ref bool enabled,
            ref bool folded,
            out Rect reorderInteractionRect,
            out Rect secondaryInteractionRect
            )
        {
            Color color = Color.white;

            Event ev = Event.current;

            Rect backgroundRect = GUILayoutUtility.GetRect(4f, 17f);

            Vector2 textDimensions = EditorStyles.boldLabel.CalcSize(new GUIContent(title));

            float offset = 3f;
            float currentX = offset;

            reorderInteractionRect = backgroundRect;
            reorderInteractionRect.x = backgroundRect.x + currentX;
            reorderInteractionRect.y += 5f;
            reorderInteractionRect.width = 9f;
            reorderInteractionRect.height = 9f;

            // Background rect should be full width
            backgroundRect.xMin -= 3f;
            backgroundRect.yMin -= 2f;
            backgroundRect.width += 3f;
            backgroundRect.height += 2f;

            currentX += 2f;

            // Reorderable button
            for (int i = 0; i < 3; ++i)
            {
                Rect currentInteractionRect = reorderInteractionRect;
                currentInteractionRect.height = 1;
                currentInteractionRect.y = reorderInteractionRect.y + reorderInteractionRect.height * (i / 3.0f);

                EditorGUI.DrawRect(currentInteractionRect, ReorderableHeaderStyles.ReorderColor);
            }

            // Foldout
            if (canBeFolded)
            {
                currentX += 13f;

                Rect foldoutRect = backgroundRect;
                foldoutRect.y += 4f;
                foldoutRect.x += currentX;
                foldoutRect.width = 13f;
                foldoutRect.height = 13f;

                folded = !GUI.Toggle(foldoutRect, !folded, GUIContent.none, EditorStyles.foldout);
            }
            else
            {
                folded = false;
            }

            // Disabled checkbox
            if (canBeDisabled)
            {
                currentX += 16;

                Rect toggleRect = backgroundRect;
                toggleRect.x += currentX;
                toggleRect.y += 4f;
                toggleRect.width = 13f;
                toggleRect.height = 13f;

                // Active checkbox
                enabled = GUI.Toggle(toggleRect, enabled, GUIContent.none, ReorderableHeaderStyles.SmallTickbox);

                currentX += 4;
            }
            else
            {
                enabled = true;
            }

            Rect labelRect = Rect.zero;

            // Title
            using (new EditorGUI.DisabledScope(!enabled))
            {
                currentX += 15;

                labelRect = backgroundRect;
                labelRect.x += currentX;
                labelRect.xMax = backgroundRect.xMax - 20f;

                EditorGUI.LabelField(labelRect, title, EditorStyles.boldLabel);
            }

            currentX += textDimensions.x;

            // Extra title
            Rect extraTitleRect = backgroundRect;
            extraTitleRect.x += currentX;
            extraTitleRect.xMax = backgroundRect.xMax - 20f;

            EditorGUI.LabelField(extraTitleRect, extraTitle);

            Rect menuRect = new Rect(backgroundRect.xMax - 17, backgroundRect.y - 3f, 16, 20);

            // Generic menu
            EditorGUI.LabelField(menuRect, "...", EditorStyles.boldLabel);

            if (ev.type == EventType.MouseDown)
            {
                if (menuRect.Contains(ev.mousePosition))
                {
                    showGenericMenu?.Invoke();
                    ev.Use();
                }
            }

            if (ev.type == EventType.MouseDown && labelRect.Contains(ev.mousePosition) && ev.button == 0)
            {
                folded = !folded;
                ev.Use();
            }

            secondaryInteractionRect = backgroundRect;
        }
    }
}