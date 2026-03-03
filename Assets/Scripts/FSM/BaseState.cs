using System;
using FSM.StateMachines;

namespace FSM
{
    public abstract class BaseState
    {
        public virtual Type StateMachineType { get; protected set; }
        protected BaseStateMachine StateMachine;

        public void Initialize(BaseStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        public abstract void Enter();

        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnLateUpdate() { }
        public abstract void Exit();
    }
}