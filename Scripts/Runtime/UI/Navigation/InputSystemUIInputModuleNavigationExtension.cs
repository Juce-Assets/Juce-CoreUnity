using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using static UnityEngine.InputSystem.InputAction;

namespace Juce.CoreUnity.Ui.Others.Navigation
{
    public class InputSystemUIInputModuleNavigationExtension : MonoBehaviour
    {
        [SerializeField] private InputSystemUIInputModule inputSystemUIInputModule = default;

        private GameObject lastSelectedSelectable;
        private GameObject toSelect;

        private bool wasOver;

        private void Awake()
        {
            inputSystemUIInputModule.move.action.performed += OnMovePerformed;
        }

        private void OnDestroy()
        {
            inputSystemUIInputModule.move.action.performed -= OnMovePerformed;
        }

        private void Update()
        {
            RegisterLastSelected();

            TryDeselectIfMouseInput();
        }

        private void LateUpdate()
        {
            TrySelectDesignated();
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

            if(EventSystem.current.currentSelectedGameObject == null)
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

            if(EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }

            if(lastSelectedSelectable == null)
            {
                return;
            }

            toSelect = lastSelectedSelectable;
        }

        private void TrySelectDesignated()
        {
            if(toSelect == null)
            {
                return;
            }

            EventSystem.current.SetSelectedGameObject(toSelect);

            toSelect = null;
        }

        private void TryDeselectIfMouseInput()
        {
            bool isOver = EventSystem.current.IsPointerOverGameObject();

            bool lastWasOver = wasOver;
            wasOver = isOver;

            if (!isOver)
            {
                return;
            }

            if(!lastWasOver)
            {
                return;
            }

            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
