using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MultiJam
{
    public class CardViewIdle : BaseCardViewState
    {
        Vector3 DefaultSize { get; }

        public CardViewIdle(ICardView handler, BaseStateMachine fsm, CardViewParameters parameters) : base(handler, fsm, parameters)
        {
            DefaultSize = Handler.transform.localScale;
        }

        public override void OnEnterState()
        {
            Handler.Input.OnPointerEnter -= OnPointerEnter;
            Handler.Input.OnPointerEnter += OnPointerEnter;
            Handler.Input.OnPointerDown -= OnPointerDown;
            Handler.Input.OnPointerDown += OnPointerDown;

            if(Handler.Movement.IsOperating)
            {
                
                DisableCollision();
                Handler.Movement.OnFinishMotion -= Enable;
                Handler.Movement.OnFinishMotion += Enable;
            }
            else
            {
                Enable();
            }

            MakeRenderNormal();
            Handler.ScaleTo(DefaultSize, Parameters.ScaleSpeed);
        }

        public override void OnExitState()
        {
            Handler.Input.OnPointerEnter -= OnPointerEnter;
            Handler.Input.OnPointerDown -= OnPointerDown;

            Handler.Movement.OnFinishMotion -= Enable;
        }

        void OnPointerEnter(PointerEventData obj)
        {
            if (FSM.IsCurrent(this))
                Handler.Hover();
        }

        void OnPointerDown(PointerEventData obj)
        {
            if (FSM.IsCurrent(this))
                Handler.Select();
        }
    }
}