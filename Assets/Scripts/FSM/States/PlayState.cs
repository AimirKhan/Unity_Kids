using UnityEngine;

namespace FSM.States
{
    public class PlayState : GameplayStates
    {

        public override void Enter()
        {
            Debug.Log("Main gameplay started!");
        }

        public override void Exit()
        {
            
        }
    }
}