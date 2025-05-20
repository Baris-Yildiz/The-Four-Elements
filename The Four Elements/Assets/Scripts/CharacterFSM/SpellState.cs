using System.Linq;
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
        events.Clear();
        events.Add(0.28f,player.playerSkills.projectileSkill.Activate);
        state.Events(state).OnEnd = null;
        state.Events(state).OnEnd += (SetNextState);
    }

    void SetNextState()
    {
        if ((player.IsCombatState && player.GetSwordState()) || !player.IsCombatState)
        {
            stateMachine.ChangeState(player.NonCombatMoveState);
        }
        else
        {
            stateMachine.ChangeState(player.KatanaMoveState);
        }
    }

    public override void Update()
    {   
        /*
        if (player.target != null)
        {
            Vector3 directionToTarget = player.target.position - player.transform.position;
            directionToTarget.y = 0;
            if (directionToTarget.sqrMagnitude > 0.001f)
            {
            
                Vector3 newDirection = Vector3.RotateTowards(player.transform.forward, directionToTarget,Mathf.Infinity,0f);
                player.transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
        */

        player.KatanaMoveState.SetSpeed();
        base.Update();
    }

    public override void Exit()
    {
        animancer.Layers[2].StartFade(0 , 0.2f);
        base.Exit();
    }
}
