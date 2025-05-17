using Animancer;
using UnityEngine;

public class EnemyDeathState:EnemyState
{
    public EnemyDeathState(EnemyFSMController enemy, EnemyStateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer) : base(enemy, stateMachine, stateClips, animancer)
    {
    }

    public override void Enter()
    {
        base.Enter();
        int n = animationClips.Length;
        animancer.Animator.applyRootMotion = true;
        AnimancerState state = animancer.Play(animationClips[Random.Range(0, n)], 0.2f);
        state.Events(state).OnEnd = (() =>
        {
            Debug.Log("Died");
            enemy.gameObject.GetComponent<Entity>().Die();
        });
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
