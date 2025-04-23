using Animancer;
using UnityEngine;

public class AirState : State
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

   

    public AirState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips,AnimancerComponent animancer) : base(player, animationParameter, stateMachine, stateClips,animancer)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
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
