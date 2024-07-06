using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MultiJam
{
    public class MouseInputProvider : MonoBehaviour, IMouseInput
    {
        // TODO : Consider to impletment Save Infokes
        public Action<PointerEventData> OnPointerClick { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Action<PointerEventData> OnPointerDown { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Action<PointerEventData> OnPointerUp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Action<PointerEventData> OnBeginDrag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Action<PointerEventData> OnDrag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Action<PointerEventData> OnEndDrag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Action<PointerEventData> OnDrop { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Action<PointerEventData> OnPointerEnter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Action<PointerEventData> OnPOinterExt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Vector2 MousePosition => throw new NotImplementedException();

        public DragDirection DragDirection => throw new NotImplementedException();
    }
}