using UnityEngine;

namespace MultiJam
{
    public class CardViewHandFsm : BaseStateMachine
    {
        #region Properties & Fields

        CardViewIdle IdleState { get; }
        CardViewHover HoverState { get; }
        CardViewDraw DrawState { get; }
        CardViewSelect SelectState { get; }


        CardViewParameters CardConfigsParameters { get; }

        #endregion

        #region Constructor

        public CardViewHandFsm(Camera camera, CardViewParameters cardConfigsParameters, ICardView handler = null) : base(handler)
        {
            CardConfigsParameters = cardConfigsParameters;

            IdleState = new CardViewIdle(handler, this, cardConfigsParameters);
            HoverState = new CardViewHover(handler, this, cardConfigsParameters);
            DrawState = new CardViewDraw(handler, this, cardConfigsParameters);
            SelectState = new CardViewSelect(handler, this, cardConfigsParameters);

            RegisterState(IdleState);
            RegisterState(HoverState);
            RegisterState(DrawState);
            RegisterState(SelectState);

            Initialize();
        }

        #endregion

        #region Operations

        public void Enable() => PushState<CardViewIdle>();

        public void Disable()
        {

        }

        public void Draw() => PushState<CardViewDraw>();

        public void Discard()
        {

        }

        public void Select()
        {

        }

        public void Unselect()
        {

        }

        public void Hover() => PushState<CardViewHover>();

        #endregion
    }
}