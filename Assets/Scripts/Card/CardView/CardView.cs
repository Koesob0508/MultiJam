using UnityEngine;

namespace MultiJam
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(IMouseInput))]
    public class CardView : MonoBehaviour, ICardView
    {
        #region State
        
        public bool IsDragging => throw new System.NotImplementedException();

        public bool IsHovering => throw new System.NotImplementedException();

        public bool IsDisabled => throw new System.NotImplementedException();

        public bool IsPlayer => throw new System.NotImplementedException();

        #endregion

        #region Motion

        public CardViewMotionBase Movement { get; private set; }
        public CardViewMotionBase Rotation { get; private set; }
        public CardViewMotionBase Scale { get; private set; }

        #endregion

        #region Components

        public Camera MainCamera => Camera.main;

        public SpriteRenderer[] Renderers { get; set; }

        public SpriteRenderer Renderer { get; set; }

        public Collider Collider { get; set; }

        public Rigidbody Rigidbody { get; set; }

        public IMouseInput Input { get; set; }

        public MonoBehaviour MonoBehaviour => this;

        #endregion

        public void Disable()
        {
            throw new System.NotImplementedException();
        }

        public void Discard()
        {
            throw new System.NotImplementedException();
        }

        public void Draw()
        {
            throw new System.NotImplementedException();
        }

        public void Enable()
        {
            throw new System.NotImplementedException();
        }

        public void Hover()
        {
            throw new System.NotImplementedException();
        }

        public void MoveTo(Vector3 position, float speed, float delay = 0)
        {
            throw new System.NotImplementedException();
        }

        public void MoveToWithZ(Vector3 position, float speed, float delay = 0)
        {
            throw new System.NotImplementedException();
        }

        public void RotateTo(Vector4 euler, float speed, float delay = 0)
        {
            throw new System.NotImplementedException();
        }

        public void ScaleTo(Vector3 scale, float speed, float delay = 0)
        {
            throw new System.NotImplementedException();
        }

        public void Select()
        {
            throw new System.NotImplementedException();
        }

        public void UnSelect()
        {
            throw new System.NotImplementedException();
        }
    }
}