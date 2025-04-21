using UnityEngine;

public class AttackState : GroundState
{
    private int attack = 0;
    private bool nextAttack = false;
    public AttackState(Player player, string animationParameter, StateMachine stateMachine) : base(player, animationParameter, stateMachine)
    {
    }

    public override void Enter()
    {
        player.animator.SetInteger(animationParameter , ++attack);
        player._controller._input.leftAttack = false;
        player._controller.SetMoveSpeedMultiplier(0.1f);
    }

    public override void Update()
    {
       // Debug.Log(player._controller._input.leftAttack);
        
        if (player._controller._input.leftAttack)
        {
            nextAttack = true;
            player._controller._input.leftAttack = false;
        }
        

        if (player.PlayerEvent().AttackEnded())
        {
            //Debug.Log("hererere " + attack);
            if (nextAttack)
            {
                //Debug.Log("next ATTack");
                attack++;
                if (attack == player.maxCombo+1)
                {
                    attack = 1;
                }
                player.animator.SetInteger(animationParameter,attack);
                nextAttack = false;
                player.PlayerEvent().AttackChange();
                //Debug.Log(player.PlayerEvent().AttackEnded()  + "  current attack : " + attack);
            }
            else
            {
                //Debug.Log("Changing to move");
                stateMachine.ChangeState(player.moveState);
            }
            
        }
    }

    public override void Exit()
    {
        ResetAttackState();
    }

    private void ResetAttackState()
    {
        attack = 0;
        player.animator.SetInteger(animationParameter , attack);
        nextAttack = false;
        player._controller._input.leftAttack = false;
        player.PlayerEvent().AttackChange();
        player._controller.SetMoveSpeedMultiplier(1f);
        
    }
}
