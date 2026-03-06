using FSM.States;

namespace FSM.StateMachines
{
    public class GameplayStateMachine : BaseStateMachine
    {
        public void Initialize()
        {
            
            ChangeState<BootstrapState>();
        }
    }
}