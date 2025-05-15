using Animancer;
using UnityEngine;

public class KatanaSkillState: State
{

    public int index { get; set; } = 0;

    public KatanaSkillState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer) : base(player, animationParameter, stateMachine, stateClips, animancer)
    {
    }

    public override void Enter()
    {
        player._controller.SetMoveSpeedMultiplier(0);
        animancer.Animator.applyRootMotion = true;
        AnimancerState state = animancer.Play(animationClips[index] , 0.2f);
        player._controller._input.spell2 = false;
        state.Time = 1.5f;
        state.Events(state, out AnimancerEvent.Sequence events);
        //events.Add(0.28f,player.playerSkills.projectileSkill.Activate);
        state.Events(state).OnEnd = null;
        state.Events(state).OnEnd += (() => stateMachine.ChangeState(player.KatanaMoveState));
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

        //player.KatanaMoveState.SetSpeed();
        base.Update();
    }

    public override void Exit()
    {
        player._controller.SetMoveSpeedMultiplier(1);
        animancer.Animator.applyRootMotion = false;
       // animancer.Layers[2].StartFade(0 , 0.2f);
        base.Exit();
    }
}