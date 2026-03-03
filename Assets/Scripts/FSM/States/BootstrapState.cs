using UnityEngine;

namespace FSM.States
{
    public class BootstrapState : GameplayStates
    {
        public override void Enter()
        {
            Debug.Log("System: Initializing resources...");
            StateMachine.ChangeState<StartState>();
        }

        public override void Exit()
        {
            
        }
    }
}