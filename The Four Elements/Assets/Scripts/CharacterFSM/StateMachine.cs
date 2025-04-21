using UnityEngine;

public class StateMachine
{
    public State currentState { get; private set; }
    
    public void Initialize(State startState)
    {
        currentState = startState;
        currentState?.Enter(); // Call Enter on the initial state
    }
    public void ChangeState(State newState)
    {
        
        if (newState == null || newState == currentState)
        {
            Debug.LogWarning($"Cannot change state to null or the same state: {newState?.GetType().Name}");
            return;
        }
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    public System.Type GetCurrentStateType() => currentState?.GetType();


}
