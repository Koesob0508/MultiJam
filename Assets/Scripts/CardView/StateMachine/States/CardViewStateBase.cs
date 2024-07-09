namespace MultiJam
{
    public abstract class CardViewStateBase : IState
    {
        const int LayerToRenderNormal = 0;
        const int LayerToRenderTop = 1;

        #region Properties & Fields

        protected ICardView Handler { get; }
        protected CardViewParameters Parameters { get; }
        protected BaseStateMachine FSM { get; }

        #endregion

        #region Constructor

        protected CardViewStateBase(ICardView handler, BaseStateMachine fsm, CardViewParameters parameters)
        {
            Handler = handler;
            FSM = fsm;
            Parameters = parameters;
            IsInitialized = true;
        }

        #endregion

        #region Operations

        /// <summary>
        ///     Renders the textures in the first layer.
        ///     Each card state is responsible to handle its own layer activity.
        /// </summary>
        protected virtual void MakeRenderFirst()
        {
            for (var i = 0; i < Handler.Renderers.Length; i++)
                Handler.Renderers[i].sortingOrder = LayerToRenderTop;
        }

        /// <summary>
        ///     Renders the textures in the regular layer.
        ///     Each card state is responsible to handle its own layer activity.
        /// </summary>
        protected virtual void MakeRenderNormal()
        {
            for (var i = 0; i < Handler.Renderers.Length; i++)
                if (Handler.Renderers[i])
                    Handler.Renderers[i].sortingOrder = LayerToRenderNormal;
        }

        /// <summary>
        ///     Enables the card entirely. Collision, Rigidbody and adds Alpha.
        /// </summary>
        protected virtual void Enable()
        {
            if (Handler.Collider)
                EnableCollision();
            if (Handler.Rigidbody)
                Handler.Rigidbody.Sleep();

            MakeRenderNormal();
            RemoveAllTransparency();
        }

        /// <summary>
        ///     Disables the card entirely. Collision, Rigidbody and adds Alpha.
        /// </summary>
        protected virtual void Disable()
        {
            DisableCollision();
            Handler.Rigidbody.Sleep();
            MakeRenderNormal();
            foreach(var renderer in Handler.Renderers)
            {
                var myColor = renderer.color;
                // myColor.a = Parameters.DisabledAlpha;
                renderer.color = myColor;
            }
        }

        /// <summary>
        ///     Enables the collision with this card.
        /// </summary>
        protected void EnableCollision() => Handler.Collider.enabled = true;

        /// <summary>
        ///     Disables the collision with this card.
        /// </summary>
        protected void DisableCollision() => Handler.Collider.enabled = false;

        /// <summary>
        ///     Remove any alpha channel in all renderers.
        /// </summary>
        protected virtual void RemoveAllTransparency()
        {
            foreach (var renderer in Handler.Renderers)
                if (renderer)
                {
                    var myColor = renderer.color;
                    myColor.a = 1;
                    renderer.color = myColor;
                }
        }

        #endregion

        /// <summary>
        ///     IState interface의 구현체이긴 하지만, 이 클래스도 여전히 추상 클래스이기 때문에
        ///     IState로부터 계약 된 모든 함수들은 virtual로 지정
        /// </summary>
        #region IState

        public bool IsInitialized { get; }

        public virtual void OnInitialize() { }

        public virtual void OnEnterState() { }

        public virtual void OnUpdate() { }

        public virtual void OnExitState() { }

        public virtual void OnClear() { }

        public virtual void OnNextState(IState next) { }

        #endregion
    }
}