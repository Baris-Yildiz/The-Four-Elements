using Animancer;
using UnityEngine;

public class EnemyShootingState:EnemyState
{
    public EnemyShootingState(EnemyFSMController enemy, EnemyStateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer) : base(enemy, stateMachine, stateClips, animancer)
    {
    }

    public override void Enter()
    {
        AnimancerState state = animancer.Play(animationClips[0]);
        float duration = state.Length;
        Debug.Log(duration);
        Debug.Log( enemy._inputs.attackSpeed);
        state.Speed = duration / enemy._inputs.attackSpeed;
        animancer.Play(state, 0.05f);
        base.Enter();
    }

    public override void Update()
    {

        if (!enemy._inputs.canAttack)
        {
            stateMachine.ChangeState(enemy._locomotionState);
        }

        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
