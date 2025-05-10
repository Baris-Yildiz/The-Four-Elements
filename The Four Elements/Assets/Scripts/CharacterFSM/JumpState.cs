using Animancer;
using UnityEngine;

public class JumpState : AirState
{
   // private AnimancerState state;
    public JumpState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips,AnimancerComponent animancer) : base(player, animationParameter, stateMachine, stateClips,animancer)
    {
        
    }

    public override void Enter()
    {
       // player.animator.SetBool(animationParameter , true);
        player._controller.SetMoveSpeedMultiplier(0.5f);
        AnimancerState state = animancer.Play(animationClips[0], 0.15f , FadeMode.FixedDuration);
        state.Events(state).OnEnd = () =>
        {
            state.NormalizedTime = 0.8f;
        };
        //animancer.Play(animationClips[0], 0.15f , FadeMode.FixedDuration);
    }

    public override void Update()
    {
        if (player._controller._verticalVelocity < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        //player.animator.SetBool(animationParameter , false);
        player._controller.SetMoveSpeedMultiplier(1f);
    }
    
}
