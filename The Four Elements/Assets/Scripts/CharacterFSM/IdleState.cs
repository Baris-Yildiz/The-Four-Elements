using System;
using UnityEngine;

public class IdleState : GroundState
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame


    public IdleState(Player player, string animationParameter, StateMachine stateMachine) : base(player, animationParameter, stateMachine)
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
            stateMachine.ChangeState(player.moveState);
            
        }
    }

    public override void Exit()
    {
        player.animator.SetFloat(animationParameter,player._controller._speed);
    }


    
}
