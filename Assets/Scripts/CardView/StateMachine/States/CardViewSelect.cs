using UnityEngine;
using UnityEngine.EventSystems;

namespace MultiJam
{
    public class CardViewSelect : BaseCardViewState
    {
        private Vector3 mousePos;
        private Vector3 offset;
        private Plane plane;

        public CardViewSelect(ICardView handler, BaseStateMachine fsm, CardViewParameters parameters) : base(handler, fsm, parameters)
        {
        }

        #region Pointer Operations

        void OnPointerDown(PointerEventData eventData)
        {
            if(FSM.IsCurrent(this) && eventData.button == PointerEventData.InputButton.Right)
            {
                DisableCollision();
                FSM.PopState();
            }

            if (FSM.IsCurrent(this) && eventData.button == PointerEventData.InputButton.Left)
            {
                Debug.Log("Play");
                FSM.PopState();
            }
        }

        #endregion

        #region State Operations

        public override void OnEnterState()
        {
            Handler.Input.OnPointerDown -= OnPointerDown;
            Handler.Input.OnPointerDown += OnPointerDown;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Handler.Input.MousePosition);
            offset = Handler.transform.position - new Vector3(mousePos.x, mousePos.y, Handler.transform.position.z);
        }

        public override void OnExitState()
        {
            Handler.Input.OnPointerDown -= OnPointerDown;

            DisableCollision();
        }

        public override void OnUpdate() => FollowCursor();

        #endregion

        #region Utils

        private void FollowCursor()
        {
            plane = new Plane(-Vector3.forward, Handler.transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Handler.Input.MousePosition);
            if (plane.Raycast(ray, out float enter))
                mousePos = ray.GetPoint(enter);

            Handler.transform.position = mousePos;
        }

        #endregion
    }
}