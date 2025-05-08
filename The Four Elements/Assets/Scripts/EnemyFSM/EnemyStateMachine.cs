using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }
    
    public void Initialize(EnemyState startState)
    {
        currentState = startState;
        currentState?.Enter(); // Call Enter on the initial state
    }
    public void ChangeState(EnemyState newState)
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