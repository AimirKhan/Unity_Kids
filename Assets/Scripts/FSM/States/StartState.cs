using UnityEngine;

namespace FSM.States
{
    public class StartState : GameplayStates
    {
        
        public override void Enter()
        {
            Debug.Log("Start gameplay");
            //StateMachine.ChangeState(playState);
        }

        public override void Exit()
        {
            
        }
    }
}