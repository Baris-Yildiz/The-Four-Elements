using System;
using Animancer;
using UnityEngine;

public class CombatIdle : GroundState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame


    public CombatIdle(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips,AnimancerComponent animancer) : base(player, animationParameter, stateMachine, stateClips,animancer)
    {
    }

    public override void Enter()
    {
       
    }

    public override void Update()
    {
        
        
    }

    public override void Exit()
    {
        
    }

}
