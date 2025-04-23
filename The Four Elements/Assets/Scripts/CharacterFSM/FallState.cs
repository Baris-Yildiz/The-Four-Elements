using Animancer;
using UnityEngine;

public class FallState : AirState
{
    public FallState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips,AnimancerComponent animancer) : base(player, animationParameter, stateMachine, stateClips,animancer)
    {
    }

    public override void Enter()
    {
       // player.animator.SetBool(animationParameter  , true);
        player._controller.SetMoveSpeedMultiplier(0f);
        animancer.Play(animationClips[1],0.1f);
        
    }

    public override void Update()
    {
        if (player._controller.Grounded)
        {
           AnimancerState state = animancer.Play(animationClips[2],0.2f);
           state.Events(state).OnEnd ??= ChangeToMove;
        }
    }

    public override void Exit()
    {
        //player.animator.SetBool(animationParameter  , false);
        player._controller.SetMoveSpeedMultiplier(1f);
    }

    private void ChangeToMove()
    {
        if (player.IsCombatState)
        {
            stateMachine.ChangeState(player.KatanaMoveState);
        }
        else
        {
            stateMachine.ChangeState(player.NonCombatMoveState);
        }
    }
}
