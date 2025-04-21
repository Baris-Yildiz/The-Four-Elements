using UnityEngine;

public class AirState : State
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public AirState(Player player, string animationParameter, StateMachine stateMachine) : base(player, animationParameter, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
