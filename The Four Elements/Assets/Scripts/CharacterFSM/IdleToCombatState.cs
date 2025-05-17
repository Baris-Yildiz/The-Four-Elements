using Animancer;
using UnityEngine;

public class IdleToCombatState: State
{
    private bool _requestNextAttack = false;
    public float remainingTime { get; set; } = 6f;

    public IdleToCombatState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer) : base(player, animationParameter, stateMachine, stateClips, animancer)
    {
        
    }

    public override void Enter()
    {
        //Debug.Log("ENTERING IDLE TO COMBAT STATE");
        remainingTime = 3f;
        player._controller.SetMoveSpeedMultiplier(0.8f);
        player._controller._input.leftAttack = false;
        AnimancerState state = null; 
        state = animancer.Layers[1].Play(animationClips[0], 0.3f);
        state.Events(state, out AnimancerEvent.Sequence events);
        events.Clear();
        events.Add(0.15f, player.UnsheatSword);
        player.KatanaMoveState.PlayLocomotion();
        state.Events(state).OnEnd = null;
        state.Events(state).OnEnd = ChangeToAttack;
    }

    public override void Update()
    {
        player.KatanaMoveState.SetSpeed();
        if (player._controller._input.leftAttack)
        {
            _requestNextAttack = true;
            player._controller._input.leftAttack = false; 
        }

        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0 && !player.IsCombatState)
        {
            ChangeToMove();
        }
    }

    public override void Exit()
    {
        _requestNextAttack = false;
        player._controller._input.leftAttack = false;
        //animancer.Layers[1].Weight = 0f;
        animancer.Layers[1].StartFade(0 , 0.3f);
       // player.KatanaMoveState.StopLocomotion();
        player._controller.SetMoveSpeedMultiplier(1f);
    }

    void ChangeToMove()
    {
        stateMachine.ChangeState(player.combatToIdleState);
    }

    void ChangeToAttack()
    {
        if (_requestNextAttack)
        {
            stateMachine.ChangeState(player.attackState);    
        }
        else
        {
            stateMachine.ChangeState(player.KatanaMoveState);
        }
    }
}
