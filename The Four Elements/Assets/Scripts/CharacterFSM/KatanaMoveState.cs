using Animancer;
using UnityEngine;

public class KatanaMoveState: MoveState
{
    public KatanaMoveState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer, float walkingT, float runningT) : base(player, animationParameter, stateMachine, stateClips, animancer, walkingT, runningT)
    {
    }

    public override void Enter()
    {
        //Debug.Log("ENTERING KATANA MOVE STATE");
        base.Enter();
    }

    public override void Update()
    {
        player.idleToCombatState.remainingTime -= Time.deltaTime;

        if (player._controller._input.spell1)
        {
            player.stateMachine.ChangeState(player.spellState);
            player._controller._input.spell1 = false;
            return;
        }

        if (player._controller._input.leftAttack)
        {
            
            stateMachine.ChangeState(player.attackState);
            return;
        }
        if (!player.IsCombatState && player.idleToCombatState.remainingTime <=0)
        {
            stateMachine.ChangeState(player.combatToIdleState);
            return;
        }
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
