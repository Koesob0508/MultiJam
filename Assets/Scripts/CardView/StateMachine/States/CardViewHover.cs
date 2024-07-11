using UnityEngine.EventSystems;

namespace MultiJam
{
    public class CardViewHover : BaseCardViewState
    {
        public CardViewHover(ICardView handler, BaseStateMachine fsm, CardViewParameters parameters) : base(handler, fsm, parameters)
        {
        }

        #region StateOperations

        public override void OnEnterState()
        {

        }

        public override void OnExitState()
        {

        }

        #endregion

        #region PointerOperations

        void OnPointerExit(PointerEventData obj)
        {
            if (FSM.IsCurrent(this))
                FSM.PopState();
        }

        void OnPointerDown(PointerEventData obj)
        {
            if (FSM.IsCurrent(this))
            {
                FSM.PopState(true);
                Handler.Select();
            }
        }

        #endregion

        #region Utils

        #endregion
    }
}