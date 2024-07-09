namespace MultiJam
{
    public interface IState
    {
        bool IsInitialized { get; }
        void OnInitialize();
        void OnEnterState();
        void OnUpdate();
        void OnExitState();
        void OnClear();
        void OnNextState(IState next);
    }
}