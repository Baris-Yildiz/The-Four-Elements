using UnityEngine;

public class FallState : AirState
{
    public FallState(Player player, string animationParameter, StateMachine stateMachine) : base(player, animationParameter, stateMachine)
    {
    }

    public override void Enter()
    {
        player.animator.SetBool(animationParameter  , true);
        player._controller.SetMoveSpeedMultiplier(0.5f);
    }

    public override void Update()
    {
        if (player._controller.Grounded)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }

    public override void Exit()
    {
        player.animator.SetBool(animationParameter  , false);
        player._controller.SetMoveSpeedMultiplier(1f);
    }
}
