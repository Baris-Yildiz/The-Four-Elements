using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class EnemyInputs : MonoBehaviour
{
    [field: SerializeField] public float attackRange { get; private set; }
    [field: SerializeField] public float attackSpeed { get; set; }
    [SerializeField] private float rangeOffset;
    public Vector3 velocity { get; set; }
    public bool playerDetected { get; set; }
    public bool chasePlayer { get; set; }
    public bool canAttack { get; set; }
    public bool startRotation { get; set; } = false;
    public bool rotationCompleted { get; set; } = false;
    public Vector3 lastPosition { get; set; }
    public Vector3 hitPosition { get; set;}
    public float angle {get; set;}
   [field:SerializeField] public float rotationSpeed { get; set; }


    private EntityHitManager _entityHitManager;
    public Vector3 hitDirection { get; set; }
    public bool gotHit { get; set; } = false;
    
    
    
    private void Awake()
    {
        canAttack = false;
        playerDetected = false;
        lastPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        hitPosition = transform.position;
        _entityHitManager = GetComponent<EntityHitManager>();
    }

    private void OnEnable()
    {
        _entityHitManager.OnGotHit += SetHitStatus;
    }

    private void OnDisable()
    {
        _entityHitManager.OnGotHit -= SetHitStatus;
    }

    private void Update()
    {
        //Debug.LogWarning(canAttack);
        //Debug.Log(velocity);
        if (!playerDetected)
        {
            startRotation = false;
            canAttack = false;
        }
        float attackRangeSqr = attackRange * attackRange;

        if (playerDetected && (lastPosition - hitPosition).sqrMagnitude > attackRangeSqr)
        {
            print("1");
            startRotation = false;
            canAttack = false;
            CalculateHitPosition();
        } else if (playerDetected && (lastPosition - hitPosition).sqrMagnitude <= attackRangeSqr) //necis
        {
            startRotation = true;
            canAttack = false;
        }

        if ((hitPosition - transform.position).sqrMagnitude <= 0.2f * 0.2f)
        {
            
            startRotation = true;
            canAttack = angle <5f && angle > -5f; 
        }
    }

    public void SetHitStatus()
    {
        gotHit = true;
        startRotation = false;
        canAttack = false;
    }

    public void CalculateHitPosition()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float offset = attackRange - rangeOffset;
        hitPosition = lastPosition + new Vector3(offset * Mathf.Cos(angle), 0, offset * Mathf.Sin(angle));
    }
}