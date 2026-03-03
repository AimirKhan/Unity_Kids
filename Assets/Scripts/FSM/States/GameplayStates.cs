using System;
using FSM.StateMachines;

namespace FSM.States
{
    public class GameplayStates : BaseState
    {
        public override Type StateMachineType { get; protected set; } =  typeof(GameplayStateMachine);

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}