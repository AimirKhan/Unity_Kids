using Game.SquaresScroll;
using Reflex.Attributes;
using UnityEngine;

namespace FSM.States
{
    public class BootstrapState : GameplayStates
    {
        [Inject]
        private readonly SquaresScrollPresenter squaresScrollPresenter;
        
        public override void Enter()
        {
            Debug.Log("System: Initializing resources...");
            squaresScrollPresenter.InitializeElements();
            StateMachine.ChangeState<StartState>();
        }

        public override void Exit()
        {
            
        }
    }
}