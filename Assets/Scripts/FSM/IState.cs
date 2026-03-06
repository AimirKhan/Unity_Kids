namespace FSM
{
    public interface IState
    {
        void Enter();
        void OnUpdate();
        void Exit();
    }
}
