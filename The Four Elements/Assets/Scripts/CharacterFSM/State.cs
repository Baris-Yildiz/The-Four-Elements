using UnityEngine;
using System;
using Animancer;

public abstract class State
{
    protected string animationParameter;
    protected StateMachine stateMachine;
    protected AnimationClip[] animationClips;
    protected Player player;
    protected int clipIndex;
    protected AnimancerComponent animancer;

    protected State(Player player, string animationParameter, StateMachine stateMachine , AnimationClip[] stateClips,AnimancerComponent animancer)
    {
        
        this.animationParameter = animationParameter;
        this.stateMachine = stateMachine;
        this.player = player;
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
