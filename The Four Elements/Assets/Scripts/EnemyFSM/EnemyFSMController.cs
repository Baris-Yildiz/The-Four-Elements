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

    [SerializeField]private AnimationClip[] _locomotionClips;
    [SerializeField] private AnimationClip[] _shootingClips;
    [SerializeField] private float walkingT;
    [SerializeField] private float runningT;


    private void InitalizeStates()
    {
        _locomotionState = new EnemyLocomotionState(this, _stateMachine, _locomotionClips, animancer, walkingT, runningT);
        _shootingState = new EnemyShootingState(this, _stateMachine, _shootingClips, animancer);
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
        _stateMachine.currentState.Update();
    }
}
