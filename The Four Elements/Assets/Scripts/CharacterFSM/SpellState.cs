using Animancer;
using UnityEngine;

public class SpellState: State
{
    public int spellIndex { get; set; } = 0;

    public SpellState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer) : base(player, animationParameter, stateMachine, stateClips, animancer)
    {
    }

    public override void Enter()
    {
        AnimancerState state = animancer.Layers[2].Play(animationClips[spellIndex] , 0.2f);
        state.Events(state, out AnimancerEvent.Sequence events);


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
