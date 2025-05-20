using Animancer;
using UnityEngine;

public class MoveState : GroundState
{

    protected LinearMixerState linearMixerState = new LinearMixerState();
    //private LinearMixerState combatMoveState = new LinearMixerState();
    public MoveState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips,AnimancerComponent animancer, float walkingT , float runningT) : base(player, animationParameter, stateMachine, stateClips,animancer)
    {
        linearMixerState.Add(stateClips[0], 0);
        linearMixerState.Add(stateClips[1], walkingT);
        linearMixerState.Add(stateClips[2], runningT);
    }

    public override void Enter()
    {
        if (player._controller != null)
        {
           player._controller.SetMoveSpeedMultiplier(1f);  
            
        }
        else
        {
            Debug.Log("charcter controller is null");
        }

        player._controller.canJump = true;
        PlayLocomotion();
    }

    public override void Update()
    {
        SetSpeed();
        if (player._controller._verticalVelocity < -3.5f)
        {
            Debug.Log(player._controller._verticalVelocity);
            stateMachine.ChangeState(player.fallState);
        }
        else if(player._controller._input.jump)
        {
            stateMachine.ChangeState(player.jumpState);
        }
        
    }

    public override void Exit()
    {
        player._controller.canJump = false;
        base.Exit();
    }

    public void PlayLocomotion()
    {
        animancer.Play(linearMixerState, 0.3f);
    }

    public void SetSpeed()
    {
        linearMixerState.Parameter = player._controller._animationBlend;
        
    }

    public void StopLocomotion()
    {
        animancer.Play(linearMixerState , 0.3f).Time = 0;
    }
}
