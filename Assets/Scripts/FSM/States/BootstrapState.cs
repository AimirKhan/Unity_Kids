using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.SquaresScroll;
using Reflex.Attributes;
using UnityEngine;

namespace FSM.States
{
    public class BootstrapState : GameplayStates
    {
        [Inject]
        private readonly SquaresScrollPresenter squaresScrollPresenter;
        [Inject]
        private readonly CancellationToken ct;
        
        public override void Enter()
        {
            Debug.Log("System: Initializing resources...");
            
            InitSequence().Forget();
        }

        private async UniTaskVoid InitSequence()
        {
            try
            {
                await squaresScrollPresenter.InitializeElements(ct);
                StateMachine.ChangeState<StartState>();
            }
            catch (OperationCanceledException)
            {
                Debug.Log("System: Initializing was canceled.");
            }
        }
        
        public override void Exit()
        {
            
        }
    }
}