using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace MultiJam
{
    [RequireComponent(typeof(Collider))]
    public class MouseInputProvider : MonoBehaviour, IMouseInput
    {
        private Vector3 oldPosition;
        Vector2 IMouseInput.MousePosition => Input.mousePosition;
        DragDirection IMouseInput.DragDirection => GetDragDirection();

        // TODO : Consider to impletment Save Invokes
        Action<PointerEventData> IMouseInput.OnPointerClick { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnPointerDown { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnPointerUp { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnBeginDrag { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnDrag { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnEndDrag { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnDrop { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnPointerEnter { get; set; } = eventData => { };
        Action<PointerEventData> IMouseInput.OnPointerExit { get; set; } = eventData => { };

        private void Awake()
        {
            // IEventSystemHandler는 PhysicsRaycaster에 의해 호출된다. 이 Component가 없으면 안됨.
            if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
                throw new Exception(GetType() + " needs an " + typeof(PhysicsRaycaster) + " on the MainCamera");
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

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData) =>
            ((IMouseInput)this).OnPointerClick.Invoke(eventData);

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) =>
            ((IMouseInput)this).OnBeginDrag.Invoke(eventData);

        void IDragHandler.OnDrag(PointerEventData eventData) =>
            ((IMouseInput)this).OnDrag.Invoke(eventData);

        void IEndDragHandler.OnEndDrag(PointerEventData eventData) =>
            ((IMouseInput)this).OnEndDrag.Invoke(eventData);

        void IDropHandler.OnDrop(PointerEventData eventData) =>
            ((IMouseInput)this).OnDrop.Invoke(eventData);

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData) =>
            ((IMouseInput)this).OnPointerDown.Invoke(eventData);

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData) =>
            ((IMouseInput)this).OnPointerUp.Invoke(eventData);

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) =>
            ((IMouseInput)this).OnPointerEnter.Invoke(eventData);

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData) =>
            ((IMouseInput)this).OnPointerExit.Invoke(eventData);
    }
}