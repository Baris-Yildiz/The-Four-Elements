using Animancer;
using UnityEngine;

public class CombatToIdleState : State
{
    public CombatToIdleState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer) : base(player, animationParameter, stateMachine, stateClips, animancer)
    {
    }

    public override void Enter()
    {
        //Debug.Log("ENTERING COMBAT TO IDLE STATE");
        player._controller.SetMoveSpeedMultiplier(0.8f);
        AnimancerState state = null;
        state = animancer.Layers[1].Play(animationClips[1], 0.3f);
        state.Events(state, out AnimancerEvent.Sequence events);
        events.Clear();
        events.Add(0.52f, player.SheatSword);
        events.Add(0.8f, () => { stateMachine.ChangeState(player.NonCombatMoveState); });
       // animancer.Layers[1].SetWeight(0.8f);
        player.NonCombatMoveState.PlayLocomotion();
       // state.Events(state).OnEnd = null;
       // state.Events(state).OnEnd += () => { stateMachine.ChangeState(player.NonCombatMoveState); };
    }

    public override void Update()
    {
        base.Update();
        player.NonCombatMoveState.SetSpeed();
    }

    public override void Exit()
    {
        player._controller.SetMoveSpeedMultiplier(1f);
        //animancer.Layers[1].Weight = 0f;
        animancer.Layers[1].StartFade(0 , 0.3f);
       // player.NonCombatMoveState.StopLocomotion();
        base.Exit();
    }
}
