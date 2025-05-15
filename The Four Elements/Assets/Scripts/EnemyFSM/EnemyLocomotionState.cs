using Animancer;
using UnityEngine;

public class EnemyLocomotionState: EnemyState
{
    private LinearMixerState linearMixerState = new LinearMixerState();
    public EnemyLocomotionState(EnemyFSMController enemy, EnemyStateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer , float walkingT , float runningT) : base(enemy, stateMachine, stateClips, animancer)
    {
        linearMixerState.Add(stateClips[0], 0);
        linearMixerState.Add(stateClips[1], walkingT);
        linearMixerState.Add(stateClips[2], runningT);
    }

    public override void Enter()
    {
        animancer.Play(linearMixerState, 0.3f);
        base.Enter();
    }

    public override void Update()
    {
        linearMixerState.Parameter = enemy._inputs.velocity.magnitude;
        if (enemy._inputs.startRotation)
        {
            stateMachine.ChangeState(enemy._enemyRotationState);
        }

        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
