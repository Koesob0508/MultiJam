using System;
using System.Collections.Generic;

namespace MultiJam
{
    public abstract class BaseStateMachine
    {
        #region Constructor
        /// <summary>
        ///     Constructor for the state machine. A handler is optional.
        /// </summary>
        /// <param name="handler"></param>
        protected BaseStateMachine(IStateMachineHandler handler = null) => Handler = handler;
        #endregion

        #region Property

        /// <summary>
        ///     Handler for the FSM. Usually the Monobehaviour which holds this FSM.
        /// </summary>
        public IStateMachineHandler Handler { get; set; }

        /// <summary>
        ///     Boolean that indicates whether the FSM has been initialized or not.
        /// </summary>
        public bool IsInitialized { get; protected set; }

        /// <summary>
        ///     Stack of States.
        /// </summary>
        readonly Stack<IState> stack = new Stack<IState>();

        /// <summary>
        ///     This register doesn't allow the FSM to have two states whith the same Type.
        /// </summary>
        readonly Dictionary<Type, IState> register = new Dictionary<Type, IState>();

        /// <summary>
        ///     Returns the state on the top of the stack. Can be Null.
        /// </summary>
        public IState Current => PeekState();

        #endregion

        
        #region Initialization

        public void RegisterState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException("Null is not a valid state");

            var type = state.GetType();
            register.Add(type, state);
            Managers.Logger.Log<BaseStateMachine>(Handler.Name + ", Registered : ", "black", type);
        }

        /// <summary>
        ///     Initialize all states. All states have to be registered before the initialization. See OnBeforeInitialize.
        /// </summary>
        public void Initialize()
        {
            // create staes
            OnBeforeInitialize();

            foreach (var state in register.Values)
                state.OnInitialize();

            IsInitialized = true;

            OnAfterInitialize();

            Managers.Logger.Log<BaseStateMachine>(Handler.Name + ", Initialized! ", "yellow");
        }

        /// <summary>
        ///     Create and register the states overriding this method. It happens right before the Initialization.
        /// </summary>
        protected virtual void OnBeforeInitialize() { }

        /// <summary>
        ///     If you need to do sometihng after the initialization, override this method.
        /// </summary>
        protected virtual void OnAfterInitialize() { }

        #endregion

        #region Operations

        /// <summary>
        ///     Update the FSM, consequently, updating the state on the top of the stack.
        /// </summary>
        public void Update() => Current?.OnUpdate();

        /// <summary>
        ///     Pushes a state by Type triggering OnEnterState for the pushed
        ///     state and OnExitState for the prefious state in the stack.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isSilent"></param>
        public void PushState<T>(bool isSilent = false) where T : IState
        {
            var stateType = typeof(T);
            var state = register[stateType];

            PushState(state, isSilent);
        }

        /// <summary>
        ///     Pushes state by instance of the class triggering OnEnterState for the
        ///     pushed state and if not silent OnExitState for the previous state in the stack.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="isSilent"></param>
        public void PushState(IState state, bool isSilent = false)
        {
            var type = state.GetType();

            if (!register.ContainsKey(type))
                throw new ArgumentException($"State {state} not registered yet.");

            Managers.Logger.Log<BaseStateMachine>($"{Handler.Name}, {stack.Count}, Push state : ", "green", type);

            if (stack.Count > 0 && !isSilent)
                Current?.OnExitState();

            stack.Push(state);
            state.OnEnterState();
        }

        /// <summary>
        ///     Pops a state from the stack. It triggers OnExitState for the
        ///     popped state and if not silent OnEnterState for the subsequent stacked state.
        /// </summary>
        /// <param name="isSilent"></param>
        public void PopState(bool isSilent = false)
        {
            if (Current == null) return;

            var state = stack.Pop();

            Managers.Logger.Log<BaseStateMachine>($"{Handler.Name}, {stack.Count}, Pop state : ", "purple", state.GetType());
            state.OnExitState();

            if (!isSilent)
                Current?.OnEnterState();
        }

        /// <summary>
        ///     Clears and restart the states register.
        /// </summary>
        public virtual void Clear()
        {
            foreach (var state in register.Values)
                state.OnClear();

            stack.Clear();
            register.Clear();
        }

        #endregion

        #region Utils

        /// <summary>
        ///     Checks whether a Type is the same as the Type as the current state.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsCurrent<T>() where T : IState => Current?.GetType() == typeof(T);

        /// <summary>
        ///     Checks if a an StateType is the current state.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsCurrent(IState state)
        {
            if (state == null)
                throw new ArgumentNullException("Input state can not be null.");

            return Current?.GetType() == state.GetType();
        }

        /// <summary>
        ///     Peeks a state from the stack. A peek returns null if the stack is empty. It doesn't trigger any call.
        /// </summary>
        /// <returns></returns>
        public IState PeekState() => stack.Count > 0 ? stack.Peek() : null;


        #endregion
    }
}