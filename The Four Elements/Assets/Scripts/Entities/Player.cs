using System;
using Animancer;
using StarterAssets;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private AnimationClip[] locomotionClips;
    [SerializeField] private AnimationClip[] katanaLocomotionClips;
    [SerializeField] private AnimationClip[] airStateClips;
    [SerializeField] private AnimationClip[] meleeAttackClips;
    [SerializeField] private AnimationClip[] basicSpeelClips;
    [SerializeField] private AnimationClip[] skillClips;
    [SerializeField] private AnimationClip[] stanceChangeClips;
    
    
    
    [SerializeField]private Animator anim;
    [SerializeField] private ThirdPersonController control;
    public Animator animator { get; private set; }
   [field:SerializeField] public AnimancerComponent animancer { get; private set; }
    [field:SerializeField]public ThirdPersonController _controller { get; private set; }
    [SerializeField] private PlayerEvents events;
    [SerializeField] private AvatarMask upperBodyMask;
    [SerializeField] private float walkingT = 0.3f;
    [SerializeField] private float runningT = 0.6f;
    public StateMachine stateMachine { get; private set; }
    public IdleState idleState{ get; private set; }
    public NonCombatMoveState NonCombatMoveState { get; private set; }
    public KatanaMoveState KatanaMoveState { get; private set; }
    //public MoveState moveState{ get; private set; }
    public AttackState attackState{ get; private set; }
    public JumpState jumpState{ get; private set; }
    public FallState fallState{ get; private set; }
    public IdleToCombatState idleToCombatState { get; private set; }
    public CombatToIdleState combatToIdleState { get; private set; }
    public int maxCombo { get; private set; } = 3;
    public bool IsCombatState { get; set; } = false;
    public Transform target { get; set; }
    [field: SerializeField] public float range { get; private set; }

    private void Awake()
    {
        
        stateMachine = new StateMachine();
        InitializeStates();
        stateMachine.Initialize(NonCombatMoveState);
        animancer.Layers[1].Mask = upperBodyMask;
        animancer.Layers[1].IsAdditive = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        animator = anim;
    }

    // Update is called once per frame
    void Update()
    {
        //Dodge , Dash 
        stateMachine.currentState.Update();
        
    }

    void InitializeStates()
    {
        idleState = new IdleState(this, "Speed", stateMachine , locomotionClips , animancer);
        NonCombatMoveState = new NonCombatMoveState(this, "Speed", stateMachine, locomotionClips, animancer, walkingT, runningT);
        KatanaMoveState = new KatanaMoveState(this, "Speed", stateMachine, katanaLocomotionClips, animancer, walkingT, runningT);
        //moveState = new MoveState(this, "Speed", stateMachine,locomotionClips,animancer , walkingT , runningT);
        attackState = new AttackState(this, "Attack", stateMachine,meleeAttackClips,animancer);
        jumpState = new JumpState(this, "Jump", stateMachine,airStateClips,animancer);
        fallState = new FallState(this, "Fall", stateMachine,airStateClips,animancer);
        idleToCombatState = new IdleToCombatState(this, "ChangeState", stateMachine, stanceChangeClips,animancer);
        combatToIdleState = new CombatToIdleState(this, "ChangeState", stateMachine, stanceChangeClips,animancer);
        
    }

    public PlayerEvents PlayerEvent()
    {
        return events;
    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Debug.DrawLine(new Vector3(transform.position.x , transform.position.y +0.5f , transform.position.z) , target.position , Color.black);

        }

        //Debug.DrawLine(new Vector3(transform.position.x , transform.position.y +0.5f , transform.position.z) , target.position , Color.black);
    }
}
