using System;
using Animancer;
using UnityEngine;

public class IdleState : GroundState
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame


    public IdleState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips,AnimancerComponent animancer) : base(player, animationParameter, stateMachine, stateClips,animancer)
    {
    }

    public override void Enter()
    {
        
        player.animator.SetFloat(animationParameter,player._controller._speed);
    }

    public override void Update()
    {
        if (player._controller._input.leftAttack)
        {
            stateMachine.ChangeState(player.attackState);
        }
        else if (player._controller._speed > 0)
        {
            stateMachine.ChangeState(player.NonCombatMoveState);
            
        }
    }

    public override void Exit()
    {
        player.animator.SetFloat(animationParameter,player._controller._speed);
    }


    
}
