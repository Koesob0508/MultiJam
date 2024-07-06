using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MultiJam
{
    [RequireComponent(typeof(Collider))]
    public class MouseInputProvider : MonoBehaviour, IMouseInput
    {
        private MouseInputActions inputActions;
        private Vector3 oldPosition;

        // TODO : Consider to impletment Save Infokes
        public Action<PointerInput> OnPointerClick { get; set; } = pointerInput => { Debug.Log("PointerClick"); };
        public Action<PointerInput> OnPointerDown { get; set; } = pointerInput => { Debug.Log("PointerDown"); };
        public Action<PointerInput> OnPointerUp { get; set; } = pointerInput => { Debug.Log("PointerUp"); };
        public Action<PointerInput> OnBeginDrag { get; set; } = pointerInput => { Debug.Log("BeginDrag"); };
        public Action<PointerInput> OnDrag { get; set; } = pointerInput => { Debug.Log("Drag"); };
        public Action<PointerInput> OnEndDrag { get; set; } = pointerInput => { Debug.Log("EndDrag"); };
        public Action<PointerInput> OnDrop { get; set; } = pointerInput => { Debug.Log("Drop"); };
        public Action<PointerInput> OnPointerEnter { get; set; } = pointerInput => { Debug.Log("PointerEnter"); };
        public Action<PointerInput> OnPointerExit { get; set; } = pointerInput => { Debug.Log("PointerExit"); };

        public Vector2 MousePosition => inputActions.UI.PointerPosition.ReadValue<Vector2>();

        public DragDirection DragDirection => GetDragDirection();

        private void Awake()
        {
            inputActions = new MouseInputActions();
        }

        private void OnEnable()
        {
            inputActions.UI.Enable();

            inputActions.UI.PointerClick.performed += context => OnPointerClick?.Invoke(CreatePointerInput(context));
            inputActions.UI.PointerDown.performed += context => OnPointerDown?.Invoke(CreatePointerInput(context));
            inputActions.UI.PointerUp.performed += context => OnPointerUp?.Invoke(CreatePointerInput(context));
            inputActions.UI.BeginDrag.performed += context => OnBeginDrag?.Invoke(CreatePointerInput(context));
            inputActions.UI.Drag.performed += context => OnDrag?.Invoke(CreatePointerInput(context));
            inputActions.UI.EndDrag.performed += context => OnEndDrag?.Invoke(CreatePointerInput(context));
            inputActions.UI.Drop.performed += context => OnDrop?.Invoke(CreatePointerInput(context));
            inputActions.UI.PointerEnter.performed += context => OnPointerEnter?.Invoke(CreatePointerInput(context));
            inputActions.UI.PointerExit.performed += context => OnPointerExit?.Invoke(CreatePointerInput(context));
        }

        private void OnDisable()
        {
            inputActions.UI.Disable();
        }

        /// <summary>
        /// While dragging returns the direction of the movement.
        /// </summary>
        /// <returns></returns>
        private DragDirection GetDragDirection()
        {
            var currentPosition = Input.mousePosition;
            var normalized = (currentPosition - oldPosition).normalized;

            oldPosition = currentPosition;

            if (normalized.x > 0) return DragDirection.Right;
            if (normalized.x < 0) return DragDirection.Left;
            if (normalized.y > 0) return DragDirection.Top;
            if (normalized.y < 0) return DragDirection.Down;

            return DragDirection.None;
        }

        private PointerInput CreatePointerInput(InputAction.CallbackContext context)
        {
            return new PointerInput
            {
                position = inputActions.UI.PointerPosition.ReadValue<Vector2>(),
                delta = inputActions.UI.PointerDelta.ReadValue<Vector2>(),
                scroll = inputActions.UI.PointerScroll.ReadValue<Vector2>()
            };
        }
    }
}