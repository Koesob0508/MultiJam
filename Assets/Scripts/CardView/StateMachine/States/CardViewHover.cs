using UnityEngine;
using UnityEngine.EventSystems;

namespace MultiJam
{
    public class CardViewHover : BaseCardViewState
    {
        #region Properties

        private Vector3 StartPosition { get; set; }
        private Vector3 StartEuler { get; set; }
        private Vector3 StartScale { get; set; }

        #endregion
        public CardViewHover(ICardView handler, BaseStateMachine fsm, CardViewParameters parameters) : base(handler, fsm, parameters)
        {
        }

        #region PointerOperations

        void OnPointerExit(PointerEventData eventData)
        {
            if (FSM.IsCurrent(this))
            {
                ResetValues();
                DisableCollision();

                FSM.PopState();
            }
        }

        void OnPointerClick(PointerEventData eventData)
        {
            if (FSM.IsCurrent(this) && eventData.button == PointerEventData.InputButton.Left)
            {
                FSM.PopState(true);

                FSM.PushState<CardViewSelect>();
            }
        }

        void OnPointerDown(PointerEventData eventData)
        {
            //if (FSM.IsCurrent(this) && eventData.button == PointerEventData.InputButton.Left)
            //{
            //    FSM.PopState(true);
            //    FSM.PushState<CardViewSelect>();
            //}
        }

        #endregion

        #region StateOperations

        public override void OnEnterState()
        {
            Handler.Input.OnPointerExit -= OnPointerExit;
            Handler.Input.OnPointerExit += OnPointerExit;

            Handler.Input.OnPointerClick -= OnPointerClick;
            Handler.Input.OnPointerClick += OnPointerClick;

            Handler.Input.OnPointerDown -= OnPointerDown;
            Handler.Input.OnPointerDown += OnPointerDown;

            CachePreviousValues();
            SetScale();
            SetPosition();
            SetRotation();
        }

        public override void OnExitState()
        {
            Handler.Input.OnPointerExit -= OnPointerExit;
            Handler.Input.OnPointerClick -= OnPointerClick;
            Handler.Input.OnPointerDown -= OnPointerDown;

            //ResetValues();
            //DisableCollision();
        }

        #endregion

        #region Utils

        private void CachePreviousValues()
        {
            StartScale = Handler.transform.localScale;
            StartPosition = Handler.transform.position;
            StartEuler = Handler.transform.eulerAngles;
        }

        private void ResetValues()
        {
            Handler.ScaleTo(StartScale, Parameters.ScaleSpeed);
            Handler.MoveToWithZ(StartPosition, Parameters.HoverSpeed);
            Handler.RotateTo(StartEuler, Parameters.RotationSpeed);
        }

        private void SetScale()
        {
            var currentScale = Handler.transform.localScale;
            var finalScale = currentScale * Parameters.HoverScale;

            Handler.ScaleTo(finalScale, Parameters.ScaleSpeed);
        }

        private void SetPosition()
        {
            var final = Handler.transform.position + new Vector3(0, 2f, -2);
            Handler.MoveToWithZ(final, Parameters.HoverSpeed);
        }

        private void SetRotation()
        {
            if (Parameters.HoverRotation) return;

            var speed = Handler.IsPlayer ? Parameters.RotationSpeed : Parameters.RotationSpeedP2;

            Handler.RotateTo(Vector3.zero, speed);
        }

        #endregion
    }
}