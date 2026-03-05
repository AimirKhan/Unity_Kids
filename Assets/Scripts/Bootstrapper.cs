using FSM.StateMachines;
using Reflex.Attributes;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [Inject] private GameplayStateMachine gameplaySM;
    
    private void Start()
    {
        gameplaySM.Initialize();
    }

    private void Update()
    {
        //gameplaySM.CurrentState?.OnUpdate();
    }
    
    [ContextMenu("Check Current State")]
    public void CheckCurrentState()
    {
        Debug.Log("Current State: " + gameplaySM.CurrentState);
    }
}
