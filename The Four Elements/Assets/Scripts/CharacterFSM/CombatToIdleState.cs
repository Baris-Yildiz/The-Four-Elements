using Animancer;
using UnityEngine;

public class CombatToIdleState : State
{
    public CombatToIdleState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer) : base(player, animationParameter, stateMachine, stateClips, animancer)
    {
    }

    public override void Enter()
    {
        Debug.Log("ENTERING COMBAT TO IDLE STATE");
        player._controller.SetMoveSpeedMultiplier(0.8f);
        AnimancerState state = null;
        state = animancer.Layers[1].Play(animationClips[1], 0.3f , FadeMode.FixedDuration);
        animancer.Layers[1].SetWeight(0.8f);
        player.NonCombatMoveState.PlayLocomotion();
        state.Events(state).OnEnd = null;
        state.Events(state).OnEnd += () => { stateMachine.ChangeState(player.NonCombatMoveState); };
    }

    public override void Update()
    {
        base.Update();
        player.NonCombatMoveState.SetSpeed();
    }

    public override void Exit()
    {
        player._controller.SetMoveSpeedMultiplier(1f);
        animancer.Layers[1].StartFade(0 , 0.1f);
        base.Exit();
    }
}
