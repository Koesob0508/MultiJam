using UnityEngine;

namespace MultiJam
{
    public class CardViewDraw : BaseCardViewState
    {
        Vector3 StartScale { get; set; }

        public CardViewDraw(ICardView handler, BaseStateMachine fsm, CardViewParameters parameters) : base(handler, fsm, parameters)
        {
        }

        #region State Operations

        public override void OnEnterState()
        {
            CachePreviousValue();
            DisableCollision();
            SetScale();

            Handler.Scale.OnFinishMotion += GoToIdle;
        }

        public override void OnExitState() => Handler.Scale.OnFinishMotion -= GoToIdle;

        #endregion

        #region Utils

        private void CachePreviousValue()
        {
            StartScale = Handler.transform.localScale;
            Handler.transform.localScale *= Parameters.StartSizeWhenDraw;
        }

        private void SetScale() => Handler.ScaleTo(StartScale, Parameters.ScaleSpeed);

        private void GoToIdle()
        {
            FSM.PopState(true);
            FSM.PushState<CardViewIdle>();
        }

        #endregion
    }
}