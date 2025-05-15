using Animancer;
using UnityEngine;

public class EnemyRotationState:EnemyState
{
    private int speed;
    private float direction;

    private float[] degrees;
    //private LinearMixerState _state;
    public EnemyRotationState(EnemyFSMController enemy, EnemyStateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer , float[] degrees) : base(enemy, stateMachine, stateClips, animancer)
    {
        this.degrees = degrees;
        /*
        _state = new LinearMixerState();
        for (int i = 0; i < stateClips.Length; i++)
        {
            _state.Add(stateClips[i], degrees[i]);
        }

       _state.Events(_state).OnEnd = (() =>
       {
           enemy._inputs.canAttack = true;
           stateMachine.ChangeState(enemy._shootingState);
       });
       */
    }

    public override void Enter()
    {
        //animancer.Animator.applyRootMotion = true;
        int index = GetMinDegree(degrees, enemy._inputs.angle);
       // Debug.LogWarning(degrees[index]);
        //Debug.LogWarning(enemy._inputs.angle);
        AnimancerState state = animancer.Play(animationClips[index]);
        float rotSpeed = degrees[index]/state.Length;
        
        state.Speed = Mathf.Abs(enemy._inputs.rotationSpeed / rotSpeed);
        Debug.LogWarning(state.Speed);
        state.Events(state).OnEnd = () =>
        {
           // enemy._inputs.canAttack = true;
            stateMachine.ChangeState(enemy._shootingState);
        };
        base.Enter();
    }

    public override void Update()
    {
       // Debug.LogWarning(enemy._inputs.ca);
      

        base.Update();
    }

    public override void Exit()
    {
        
        base.Exit();
    }

    private int GetMinDegree(float[] degrees,float angle)
    {
        int index = 0;
        float minDiff = Mathf.Infinity;
        for (int i = 0; i < degrees.Length; i++)
        {
            if (Mathf.Abs(degrees[i] - angle) < minDiff)
            {
                minDiff = Mathf.Abs(degrees[i] - angle);
                index = i;
            }
        }

        return index;
    }
}
