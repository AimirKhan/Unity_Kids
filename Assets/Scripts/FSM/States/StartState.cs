using UnityEngine;

namespace FSM.States
{
    public class StartState : GameplayStates
    {
        public override void Enter()
        {
            Debug.Log("Starting gameplay");
            StateMachine.ChangeState<PlayState>();
        }

        public override void Exit()
        {
            
        }
    }
}