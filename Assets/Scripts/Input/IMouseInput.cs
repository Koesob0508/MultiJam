using System;
using UnityEngine;

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

    public interface IMouseInput
    {
        // Clicks
        Action<PointerInput> OnPointerClick { get; set; }
        Action<PointerInput> OnPointerDown { get; set; }
        Action<PointerInput> OnPointerUp { get; set; }

        // Drag
        Action<PointerInput> OnBeginDrag { get; set; }
        Action<PointerInput> OnDrag { get; set; }
        Action<PointerInput> OnEndDrag { get; set; }
        Action<PointerInput> OnDrop { get; set; }

        // Enter
        Action<PointerInput> OnPointerEnter { get; set; }
        Action<PointerInput> OnPointerExit { get; set; }

        Vector2 MousePosition { get; }
        DragDirection DragDirection { get; }
    }
}