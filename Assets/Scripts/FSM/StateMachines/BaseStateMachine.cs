using System;
using System.Collections.Generic;
using Reflex.Attributes;

namespace FSM.StateMachines
{
    public abstract class BaseStateMachine
    {
        private Dictionary<Type, BaseState> states = new();
        public BaseState CurrentState { get; private set; }

        [Inject]
        protected void Initialization(IEnumerable<BaseState> injectStates)
        {
            foreach (var state in injectStates)
            {
                if (state.StateMachineType == GetType())
                {
                    state.Initialize(this);
                    states.Add(state.GetType(), state);
                }
            }
        }
        
        public void ChangeState<T>() where T : BaseState
        {
            var type = typeof(T);
            if (states.TryGetValue(type, out var newState))
            {
                CurrentState?.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
        }
    }
}