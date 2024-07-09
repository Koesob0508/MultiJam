using UnityEngine;

namespace MultiJam
{
    public class CardViewHandFsm : BaseStateMachine
    {
        #region Properties & Fields

        CardViewIdle IdleState { get; }
        CardViewParameters CardConfigsParameters { get; }

        #endregion

        #region Constructor

        public CardViewHandFsm(Camera camera, CardViewParameters cardConfigsParameters, ICardView handler = null) : base(handler)
        {
            CardConfigsParameters = cardConfigsParameters;

            IdleState = new CardViewIdle(handler, this, cardConfigsParameters);

            RegisterState(IdleState);

            Initialize();
        }

        #endregion

        #region Operations
        #endregion
    }
}