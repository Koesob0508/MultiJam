using UnityEngine;

namespace MultiJam
{
    public class CardViewIdle : CardViewStateBase
    {
        Vector3 DefaultSize { get; }

        public CardViewIdle(ICardView handler, BaseStateMachine fsm, CardViewParameters parameters) : base(handler, fsm, parameters)
        {
            DefaultSize = Handler.transform.localScale;
        }

        // TODO : StateBase의 Operatin 필요한만큼 구현
    }
}