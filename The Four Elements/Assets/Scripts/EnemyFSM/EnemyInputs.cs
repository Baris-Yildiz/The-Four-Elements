using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;
using UnityEngine.AI;

public class EnemyInputs : MonoBehaviour
{
    private EntityStats stats;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float baseAcceleration;
    [SerializeField] private float baseAttackSpeed;
    [field: SerializeField] public float navSpeed { get; private set; }
    [field: SerializeField] public float navAcceleration { get; private set; }

    [field: SerializeField] public float attackRange { get; private set; }
    [field: SerializeField] public float attackSpeed { get; set; }
    [SerializeField] private float rangeOffset;
    public Vector3 velocity { get; set; }
    public bool isDead { get; private set; } = false;
    public bool playerDetected { get; set; }
    public bool chasePlayer { get; set; }
    public bool canAttack { get; set; }
    public bool startRotation { get; set; } = false;
    public bool rotationCompleted { get; set; } = false;
    public Vector3 lastPosition { get; set; }
    public Vector3 hitPosition { get; set;}
    public int gotHitCount { get; set; }
    public float angle {get; set;}
    [SerializeField] private float accelerationAmount;
    [SerializeField] private float attackSpeedEffect;
    [field:SerializeField] public float rotationSpeed { get; set; }


    private EntityHitManager _entityHitManager;
    public Vector3 hitDirection { get; set; }
    public bool gotHit { get; set; } = false;
    
    
    
    private void Awake()
    {
        stats = GetComponent<EntityStats>();
        
        canAttack = false;
        playerDetected = false;
        lastPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        hitPosition = Vector3.positiveInfinity;
        _entityHitManager = GetComponent<EntityHitManager>();
    }

    public void SetDied()
    {
        isDead = true;
    }

    private void OnEnable()
    {
        _entityHitManager.OnGotHit += SetHitStatus;
        _entityHitManager.OnEntityDied += SetDied;
        stats.OnStatChange += ApplyStats;
    }

    private void OnDisable()
    {
        _entityHitManager.OnGotHit -= SetHitStatus;
        _entityHitManager.OnEntityDied -= SetDied;
        stats.OnStatChange -= ApplyStats;
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
        //Debug.LogWarning("player detecteddd: "+ playerDetected);

        if (playerDetected && (lastPosition - hitPosition).sqrMagnitude > attackRangeSqr)
        {
            print("1");
            startRotation = false;
            canAttack = false;
            CalculateHitPosition();
        }       

        if ((hitPosition - transform.position).sqrMagnitude <= 0.2f * 0.2f)
        {
            print("222222222");
            
            startRotation = true;
            canAttack = angle <5f && angle > -5f; 
        }
    }

    public void SetHitStatus()
    {
        gotHit = true;
        gotHitCount++;
        startRotation = false;
        canAttack = false;
    }

    void ApplyStats()
    {   
        navSpeed = baseSpeed * stats.speedMultiplier;
        navAcceleration = baseAcceleration*stats.speedMultiplier / accelerationAmount;
        attackSpeed = baseAttackSpeed * attackSpeedEffect / stats.speedMultiplier;
    }

    public void CalculateHitPosition()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float offset = attackRange - rangeOffset;

        Vector3 rawPosition = lastPosition + new Vector3(offset * Mathf.Cos(angle), 0, offset * Mathf.Sin(angle));

        NavMeshHit hit;
        if (NavMesh.SamplePosition(rawPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
  //          Debug.LogWarning("path calculated success");
            hitPosition = hit.position; 
        }
        else
        {
//            Debug.LogWarning("path calculation fail");
            hitPosition = lastPosition; 
        }
    }
}