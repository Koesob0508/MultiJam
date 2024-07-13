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

        #region State Operations

        public override void OnEnterState()
        {
            Handler.Input.OnPointerEnter -= OnPointerEnter;
            Handler.Input.OnPointerEnter += OnPointerEnter;
            Handler.Input.OnPointerClick -= OnPointerClick;
            Handler.Input.OnPointerClick += OnPointerClick;
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

            Handler.ScaleTo(DefaultSize, Parameters.ScaleSpeed);
        }

        public override void OnExitState()
        {
            Handler.Input.OnPointerEnter -= OnPointerEnter;
            Handler.Input.OnPointerClick -= OnPointerClick;
            Handler.Input.OnPointerDown -= OnPointerDown;

            Handler.Movement.OnFinishMotion -= Enable;
        }

        #endregion

        #region Pointer Operations

        void OnPointerEnter(PointerEventData obj)
        {
            if (FSM.IsCurrent(this))
                FSM.PushState<CardViewHover>();
        }

        void OnPointerClick(PointerEventData eventData)
        {
            if (FSM.IsCurrent(this))
                FSM.PushState<CardViewSelect>();
        }

        void OnPointerDown(PointerEventData obj)
        {
            if (FSM.IsCurrent(this))
                Managers.Logger.Log<CardViewIdle>($"Drag");
        }

        #endregion
    }
}