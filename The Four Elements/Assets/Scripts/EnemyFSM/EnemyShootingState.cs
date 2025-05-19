using Animancer;
using UnityEngine;

public class EnemyShootingState:EnemyState
{
    private EnemySFX _sfx;
    public EnemyShootingState(EnemyFSMController enemy, EnemyStateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer) : base(enemy, stateMachine, stateClips, animancer)
    {
        _sfx = enemy.GetComponent<EnemySFX>();
    }

    public override void Enter()
    {
        if (_sfx != null)
        {
            _sfx.PlayAttackSoundLoop(enemy._inputs.attackSpeed);
        }
        AnimancerState state = animancer.Play(animationClips[0]);
        float duration = state.Length;
//        Debug.Log(duration);
//        Debug.Log( enemy._inputs.attackSpeed);
        state.Speed = duration / enemy._inputs.attackSpeed;
        animancer.Play(state, 0.2f);
        base.Enter();
    }

    public override void Update()
    {

        if (!enemy._inputs.startRotation)
        {
            stateMachine.ChangeState(enemy._locomotionState);
        }

        base.Update();
    }

    public override void Exit()
    {
        if (_sfx != null)
        {
            _sfx.ResetLoop();    
        }

        
        base.Exit();
    }
}
