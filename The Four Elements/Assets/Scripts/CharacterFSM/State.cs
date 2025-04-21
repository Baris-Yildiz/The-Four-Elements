using UnityEngine;
using System;
public abstract class State
{
    protected string animationParameter;
    protected StateMachine stateMachine;
    protected Player player;

    protected State(Player player, string animationParameter, StateMachine stateMachine)
    {
        
        this.animationParameter = animationParameter;
        this.stateMachine = stateMachine;
        this.player = player;
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
