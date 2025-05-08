using Animancer;
using UnityEngine;

public class NonCombatMoveState: MoveState
{
    public NonCombatMoveState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer, float walkingT, float runningT) : base(player, animationParameter, stateMachine, stateClips, animancer, walkingT, runningT)
    {
    }

    public override void Enter()
    {
        //Debug.Log("ENTERING NON COMBAT MOVE STATE");
        base.Enter();
    }

    public override void Update()
    {
        if (player._controller._input.leftAttack || player.IsCombatState)
        {
            stateMachine.ChangeState(player.idleToCombatState);
            return;
        }
        

        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
