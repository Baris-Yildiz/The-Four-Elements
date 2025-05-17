using Animancer;
using UnityEngine;

public class EnemyHitState: EnemyState
{
    protected DirectionalMixerState _directionalMixerState1;
    protected DirectionalMixerState _directionalMixerState2;
    Vector2 hitDirection = Vector2.zero;
    public EnemyHitState(EnemyFSMController enemy, EnemyStateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer) : base(enemy, stateMachine, stateClips, animancer)
    {
        _directionalMixerState1 = new DirectionalMixerState();
        _directionalMixerState2 = new DirectionalMixerState();
        _directionalMixerState1.Add(stateClips[0], new Vector2(1, 0));
        _directionalMixerState1.Add(stateClips[2], new Vector2(-1, 0));
        _directionalMixerState1.Add(stateClips[4], new Vector2(0, -1));
        _directionalMixerState1.Add(stateClips[6], new Vector2(0, 1));
        
        _directionalMixerState2.Add(stateClips[1], new Vector2(1, 0));
        _directionalMixerState2.Add(stateClips[3], new Vector2(-1, 0));
        _directionalMixerState2.Add(stateClips[5], new Vector2(0, -1));
        _directionalMixerState2.Add(stateClips[7], new Vector2(0, 1));

       // _directionalMixerState1.Events(_directionalMixerState1).Add(0.1f, (() => {AudioSource.PlayClipAtPoint(); }));
    }

    public override void Enter()
    {
        int tree = Random.Range(0, 1);
        animancer.Animator.applyRootMotion = true;
        AnimancerState state;
        if (tree == 0)
        {
            _directionalMixerState1.Parameter = hitDirection;
            state =animancer.Play(_directionalMixerState1, 0.1f);
        }
        else
        {
            _directionalMixerState2.Parameter = hitDirection;
            state = animancer.Play(_directionalMixerState2, 0.1f);
        }
        state.Events(state).OnEnd =null;
        state.Events(state).OnEnd = () =>
        {
            stateMachine.ChangeState(enemy._locomotionState);
        };
        

    }
    public override void Update()
    {
        hitDirection = new Vector2(enemy._inputs.hitDirection.x, enemy._inputs.hitDirection.z);
       // Debug.Log(hitDirection + "  hit direction");
        _directionalMixerState1.Parameter = hitDirection;
        _directionalMixerState2.Parameter = hitDirection;
        
        
    }
    public override void Exit()
    {
        animancer.Animator.applyRootMotion = false;
        enemy._inputs.gotHit = false;
        enemy._inputs.CalculateHitPosition();
    }
}
