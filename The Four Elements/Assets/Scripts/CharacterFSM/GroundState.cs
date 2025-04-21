using System;
using UnityEngine;

public class GroundState : State
{
    public GroundState(Player player, string animationParameter, StateMachine stateMachine) : base(player, animationParameter, stateMachine)
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

