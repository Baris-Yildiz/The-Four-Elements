using Animancer;
using UnityEngine;
using System.Collections;

public class AttackState : GroundState
{
    private int _currentAttackIndex = 0;
    private bool _requestNextAttack = false;
    private int _maxAttacks;
    private AnimancerState _currentState;
    private Coroutine _attackMovementCoroutine;
    private bool canStartAnim = false;
    private float range;
    private bool startMoving = false;
    private int lastIndex =-1;
    public AttackState(Player player, string animationParameter, StateMachine stateMachine, AnimationClip[] stateClips, AnimancerComponent animancer)
        : base(player, animationParameter, stateMachine, stateClips, animancer)
    {
        if (stateClips == null || stateClips.Length == 0) { _maxAttacks = 0; }
        else { _maxAttacks = stateClips.Length; }

        range = player.range;
    }

    public override void Enter()
    {
        base.Enter();
       
        _requestNextAttack = false;
        player._controller._input.leftAttack = false;
        player.swordCollider.enabled = true;
        player._controller.SetMoveSpeedMultiplier(0f);
        animancer.Animator.applyRootMotion = true;
        float dist = 1000f;
        if (player.target != null)
        {
            dist = Mathf.Abs(Vector3.Distance(player.target.transform.position, player.transform.position));
        }
        if (dist > 2f + range)
        {
            PlayAttackAnimation();
            startMoving = false;
        }
        else if (dist <= 2f)
        {
            animancer.Animator.applyRootMotion = false;
            PlayAttackAnimation();
        }
        else
        {
            startMoving = true;
        }
    }

    private void PlayAttackAnimation()
    {
        if (_maxAttacks == 0)
        {
            stateMachine.ChangeState(player.KatanaMoveState);
            return;
        }
        
        // player._controller.FaceLastMovementDirection();

        AnimationClip clipToPlay = animationClips[_currentAttackIndex % _maxAttacks];
        lastIndex = _currentAttackIndex;
        _currentState = animancer.Play(clipToPlay, 0.2f, FadeMode.FixedDuration);
        _currentState.Speed = 2f;

        _currentState.Events(_currentState).OnEnd = null;
        _currentState.Events(_currentState).OnEnd += HandleAnimationEnd;
    }

    public override void Update()
    {
        if (player._controller._input.leftAttack)
        {
            _requestNextAttack = true;
            player._controller._input.leftAttack = false;
        }
        if (player.target != null)
        {
            
            float dist = Mathf.Abs(Vector3.Distance(player.target.transform.position, player.transform.position));
            Debug.Log(dist);
            if (dist > 2f && dist < 2f+range && startMoving)
            {
                RotateTowardsTarget();
                player._controller.MoveTowardsTarget(player.target.position , 8f);
                animancer.Animator.applyRootMotion = false;
            }
            else if (dist > 2f+range)
            {
                
                animancer.Animator.applyRootMotion = true;
                startMoving = false;    
                PlayAttackAnimation();
            }
            else
            {
                RotateTowardsTarget();
                animancer.Animator.applyRootMotion = false;
                PlayAttackAnimation();
            }
        }
        base.Update();
    }

    private void HandleAnimationEnd()
    {
        if (_requestNextAttack)
        {
            startMoving = true;
            _requestNextAttack = false;
            _currentAttackIndex++;
            if (_currentAttackIndex >= _maxAttacks)
            {
                _currentAttackIndex = 0;
                lastIndex = -1;
                stateMachine.ChangeState(player.KatanaMoveState);
            }
            else
            {
                PlayAttackAnimation();
            }
        }
        else
        {
            if (player.IsCombatState)
            {
                stateMachine.ChangeState(player.KatanaMoveState);
            }
            else
            {
                stateMachine.ChangeState(player.combatToIdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        startMoving = false;
        player.swordCollider.enabled = false;
        animancer.Animator.applyRootMotion = false;
        _currentAttackIndex = 0;
        _requestNextAttack = false;
        player._controller._input.leftAttack = false;
        player._controller.SetMoveSpeedMultiplier(1f);
        _currentState = null;
    }

    void RotateTowardsTarget()
    {
        Vector3 directionToTarget = player.target.position - player.transform.position;
        directionToTarget.y = 0;
        if (directionToTarget.sqrMagnitude > 0.001f)
        {
            
            Vector3 newDirection = Vector3.RotateTowards(player.transform.forward, directionToTarget,Mathf.Infinity,0f);
            player.transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
    
}
