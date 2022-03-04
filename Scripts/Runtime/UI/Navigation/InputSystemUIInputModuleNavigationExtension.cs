using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

namespace Juce.CoreUnity.Ui.Others.Navigation
{
    public class InputSystemUIInputModuleNavigationExtension : MonoBehaviour
    {
        [SerializeField] private InputSystemUIInputModule inputSystemUIInputModule = default;

        private GameObject lastSelectedSelectable;
        private GameObject toSelect;
        private GameObject nextFallbackSelectable;

        private GameObject lastUiGameObjectWasOver;
        private bool wasOver;

        public bool IsUsingSelectables { get; private set; } = false;

        public static InputSystemUIInputModuleNavigationExtension Current = null;

        private void Awake()
        {
            Current = this;

            inputSystemUIInputModule.move.action.performed += OnMovePerformed;
        }

        private void OnDestroy()
        {
            inputSystemUIInputModule.move.action.performed -= OnMovePerformed;
        }

        private void Update()
        {
            RegisterLastSelected();

            TryDeselectIfMouseOverUi();
        }

        private void LateUpdate()
        {
            TrySelectDesignated();
        }

        public void SetFallbackSelectable(Selectable fallbackSelectable)
        {
            if (fallbackSelectable == null)
            {
                return;
            }

            nextFallbackSelectable = fallbackSelectable.gameObject;
        }

        private void OnMovePerformed(CallbackContext callbackContext)
        {
            TrySelectLast();
        }

        private void RegisterLastSelected()
        {
            if (EventSystem.current == null)
            {
                return;
            }

            if (EventSystem.current.currentSelectedGameObject == null)
            {
                return;
            }

            lastSelectedSelectable = EventSystem.current.currentSelectedGameObject;
        }

        private void TrySelectLast()
        {
            if (EventSystem.current == null)
            {
                return;
            }

            if (IsUsingSelectables)
            {
                return;
            }

            if (EventSystem.current.alreadySelecting)
            {
                return;
            }

            if (nextFallbackSelectable != null)
            {
                toSelect = nextFallbackSelectable;
                nextFallbackSelectable = null;
                return;
            }

            if (lastSelectedSelectable == null)
            {
                return;
            }

            toSelect = lastSelectedSelectable;
        }

        private void TrySelectDesignated()
        {
            if (toSelect == null)
            {
                return;
            }

            IsUsingSelectables = true;

            EventSystem.current.SetSelectedGameObject(toSelect);

            toSelect = null;

            TryPointerExitIfMouseOverUi();
        }

        private void TryDeselectIfMouseOverUi()
        {
            bool isOverUi = TryGetPointerOverUiObject(out GameObject gameObject);

            if (!isOverUi)
            {
                wasOver = false;
                return;
            }

            if (lastUiGameObjectWasOver == gameObject)
            {
                lastUiGameObjectWasOver = gameObject;
                return;
            }

            lastUiGameObjectWasOver = gameObject;

            Selectable selectable = gameObject.GetComponent<Selectable>();

            bool isOver = selectable != null;

            if (!isOver)
            {
                wasOver = false;
                return;
            }

            if (wasOver)
            {
                wasOver = isOver;
                return;
            }

            wasOver = isOver;

            EventSystem.current.SetSelectedGameObject(null);

            IsUsingSelectables = false;
        }

        private void TryPointerExitIfMouseOverUi()
        {
            bool isOverUi = TryGetPointerOverUiObject(out GameObject gameObject);

            if (!isOverUi)
            {
                wasOver = false;
                return;
            }

            IPointerExitHandler[] pointerExits = gameObject.GetComponents<IPointerExitHandler>();

            if (pointerExits.Length == 0)
            {
                return;
            }

            IsUsingSelectables = true;

            foreach (IPointerExitHandler exitHandler in pointerExits)
            {
                exitHandler.OnPointerExit(new PointerEventData(EventSystem.current));
            }
        }

        public bool TryGetPointerOverUiObject(out GameObject gameObject)
        {
            bool isOver = inputSystemUIInputModule.IsPointerOverGameObject(0);

            if (!isOver)
            {
                gameObject = default;
                return false;
            }

            Vector3 mousePosition = Mouse.current.position.ReadValue();

            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(mousePosition.x, mousePosition.y);

            List<RaycastResult> results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            if (results.Count == 0)
            {
                gameObject = default;
                return false;
            }

            gameObject = results[0].gameObject;
            return true;
        }
    }
}
