using System;
using StarterAssets;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private Animator anim;
    [SerializeField] private ThirdPersonController control;
    public Animator animator { get; private set; }
    public ThirdPersonController _controller { get; private set; }
    [SerializeField] private PlayerEvents events;
    
    public StateMachine stateMachine { get; private set; }
    public IdleState idleState{ get; private set; }
    public MoveState moveState{ get; private set; }
    public AttackState attackState{ get; private set; }
    public JumpState jumpState{ get; private set; }
    public FallState fallState{ get; private set; }

    public int maxCombo { get; private set; } = 3;

    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;
    
    private void Awake()
    {
        
        stateMachine = new StateMachine();
        InitializeStates();
        stateMachine.Initialize(moveState);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = control;
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
        idleState = new IdleState(this, "Speed", stateMachine);
        moveState = new MoveState(this, "Speed", stateMachine);
        attackState = new AttackState(this, "Attack", stateMachine);
        jumpState = new JumpState(this, "Jump", stateMachine);
        fallState = new FallState(this, "Fall", stateMachine);
        
        
    }

    public PlayerEvents PlayerEvent()
    {
        return events;
    }
}
