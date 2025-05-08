using UnityEngine;
using System;
using Animancer;

public abstract class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected AnimationClip[] animationClips;
    protected Enemy enemy;
    protected int clipIndex;
    protected AnimancerComponent animancer;

    protected EnemyState( Enemy enemy,EnemyStateMachine stateMachine , AnimationClip[] stateClips,AnimancerComponent animancer)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animationClips = stateClips;
        this.animancer = animancer;
        clipIndex = 0;
    }

    public virtual void Enter()
    {
        
    }
    
    // Update is called once per frame
    public virtual void Update()
    {
       
    }
    public virtual void Exit()
    {
        
    }
}