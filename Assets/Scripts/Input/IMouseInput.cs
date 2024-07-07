using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MultiJam
{
    public struct PointerInput
    {
        public Vector2 position;
        public Vector2 delta;
        public Vector2 scroll;
    }

    public enum DragDirection
    {
        None,
        Down,
        Left,
        Top,
        Right
    }

    public interface IMouseInput :
        IPointerClickHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IDropHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        Vector2 MousePosition { get; }
        DragDirection DragDirection { get; }

        // Clicks
        new Action<PointerEventData> OnPointerClick { get; set; }
        new Action<PointerEventData> OnPointerDown { get; set; }
        new Action<PointerEventData> OnPointerUp { get; set; }

        // Drag
        new Action<PointerEventData> OnBeginDrag { get; set; }
        new Action<PointerEventData> OnDrag { get; set; }
        new Action<PointerEventData> OnEndDrag { get; set; }

        new Action<PointerEventData> OnDrop { get; set; }

        // Enter
        new Action<PointerEventData> OnPointerEnter { get; set; }
        new Action<PointerEventData> OnPointerExit { get; set; }
    }
}