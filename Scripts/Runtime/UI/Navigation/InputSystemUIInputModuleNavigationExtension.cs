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

        private void Awake()
        {
            inputSystemUIInputModule.move.action.performed += OnMovePerformed;
            inputSystemUIInputModule.leftClick.action.performed += OnClickPerformed;
        }

        private void OnDestroy()
        {
            inputSystemUIInputModule.move.action.performed -= OnMovePerformed;
            inputSystemUIInputModule.leftClick.action.performed -= OnClickPerformed;
        }

        private void LateUpdate()
        {
            TrySelectDesignated();
        }

        private void OnMovePerformed(CallbackContext callbackContext)
        {
            TrySelectLast();
        }

        private void OnClickPerformed(CallbackContext callbackContext)
        {
            RegisterLastSelected();
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
    }
}
