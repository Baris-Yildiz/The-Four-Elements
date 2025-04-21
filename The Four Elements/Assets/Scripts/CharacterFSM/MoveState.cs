using UnityEngine;

public class MoveState : GroundState
{
    public MoveState(Player player, string animationParameter, StateMachine stateMachine) : base(player, animationParameter, stateMachine)
    {
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


    }

    public override void Update()
    {
        //Debug.Log(player.animator );
        //Debug.Log(player._controller);
        
        player.animator.SetFloat(animationParameter , player._controller._animationBlend);
        
        
        
        if (player._controller._verticalVelocity < -2.5f)
        {
            stateMachine.ChangeState(player.fallState);
        }
        else if (player._controller._input.leftAttack)
        {
            stateMachine.ChangeState(player.attackState);
            
        }
        else if(player._controller._input.jump)
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
