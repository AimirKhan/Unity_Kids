using Reflex.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FSM.States
{
    public class PlayState : GameplayStates
    {
        [Inject]
        private readonly InputActionAsset inputActions;
        
        public override void Enter()
        {
            Debug.Log("Main gameplay started!"); ;
            inputActions.Enable();
        }

        public override void Exit()
        {
            
        }
    }
}