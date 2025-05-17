using System;
using Animancer;
using UnityEngine;

public class EnemyFSMController : MonoBehaviour
{
    public EnemyInputs _inputs { get; private set; }
    public float animationBlendSpeed { get; private set; } = 8f;
    private EnemyStateMachine _stateMachine;
    private AnimancerComponent animancer;

    public EnemyLocomotionState _locomotionState { get; private set; }
    public EnemyShootingState _shootingState{ get; private set; }
    public EnemyHitState _enemyHitState { get; private set; }
    public EnemyRotationState _enemyRotationState { get; private set; }
    public EnemyDeathState _enemyDeathState { get; private set; }

    [SerializeField]private AnimationClip[] _locomotionClips;
    [SerializeField] private AnimationClip[] _shootingClips;
    [SerializeField] private AnimationClip[] _enemyHitClips;
    [SerializeField] private AnimationClip[] _enemyRotationClips;
    [SerializeField] private AnimationClip[] _enemyDeathClips;
    [SerializeField] private float walkingT;
    [SerializeField] private float runningT;
    private float[] degrees = { -180, -135, -90, -45, 45, 90, 135, 180 };

    private void InitalizeStates()
    {
        _locomotionState = new EnemyLocomotionState(this, _stateMachine, _locomotionClips, animancer, walkingT, runningT);
        _shootingState = new EnemyShootingState(this, _stateMachine, _shootingClips, animancer);
        _enemyHitState = new EnemyHitState(this, _stateMachine, _enemyHitClips, animancer);
        _enemyRotationState = new EnemyRotationState(this, _stateMachine,  _enemyRotationClips,animancer, degrees);
        _enemyDeathState = new EnemyDeathState(this, _stateMachine, _enemyDeathClips, animancer);
    }

    private void Awake()
    {
        _inputs = GetComponent<EnemyInputs>();
        animancer = GetComponent<AnimancerComponent>(); ;
        _stateMachine = new EnemyStateMachine();
        InitalizeStates();
        _stateMachine.Initialize(_locomotionState);
    }
    // Update is called once per frame
    void Update()
    {
        if (_inputs.isDead)
        {
            _stateMachine.ChangeState(_enemyDeathState);
        }

        if (_inputs.gotHit && _stateMachine.currentState != _enemyHitState)
        {
            _stateMachine.ChangeState(_enemyHitState);
        }

        _stateMachine.currentState.Update();
    }
}
