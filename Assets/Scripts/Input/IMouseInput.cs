using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MultiJam
{
    public interface IMouseInput
    {
        // Clicks
        Action<PointerEventData> OnPointerClick { get; set; }
        Action<PointerEventData> OnPointerDown { get; set; }
        Action<PointerEventData> OnPointerUp { get; set; }

        // Drag
        Action<PointerEventData> OnBeginDrag { get; set; }
        Action<PointerEventData> OnDrag { get; set; }
        Action<PointerEventData> OnEndDrag { get; set; }
        Action<PointerEventData> OnDrop { get; set; }

        // Enter
        Action<PointerEventData> OnPointerEnter { get; set; }
        Action<PointerEventData> OnPOinterExt { get; set; }

        Vector2 MousePosition { get; }
        DragDirection DragDirection { get; }
    }

    public enum DragDirection
    {
        None,
        Down,
        Left,
        Top,
        Right
    }
}