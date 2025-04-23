using Animancer;
using UnityEngine;

public class SpellMoveState: MoveState
{
    public SpellMoveState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer, float walkingT, float runningT) : base(player, animationParameter, stateMachine, stateClips, animancer, walkingT, runningT)
    {
    }
}
