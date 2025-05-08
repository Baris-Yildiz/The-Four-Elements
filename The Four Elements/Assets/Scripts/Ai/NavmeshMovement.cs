using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NavmeshMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform player;
    private EnemyInputs inputs;
    [field: SerializeField] public float attackSpeed { get; set; }
    private float attackCd;
    private EntityAttackManager attackManager;
    [SerializeField]private Vector3 attackOffset;
    private EntityHitManager playerHit = null;
    private Enemy enemy;
    public float visionStr = 1f;
    [SerializeField] private Transform rayPoint;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        inputs = GetComponent<EnemyInputs>();
        enemy = GetComponent<Enemy>();
        attackManager = GetComponent<EntityAttackManager>();
        attackCd = attackSpeed;
    }
    
    // Update is called once per frame
    void Update()
    {
        RotateTowardsTarget(rayPoint);
        //Debug.Log(inputs.playerDetected +  " " + inputs.canAttack);
        if (inputs.playerDetected && inputs.chasePlayer)
        {
            agent.SetDestination(inputs.hitPosition);
        }
        else if (!inputs.playerDetected && inputs.chasePlayer)
        {
            agent.SetDestination(inputs.lastPosition);
        }
        else if (inputs.playerDetected && inputs.canAttack )
        {
            RotateTowardsTarget(transform);
            Debug.Log("rotating");
            if ((attackCd - Time.deltaTime) <= 0)
            {
                Attack();
                attackCd = attackSpeed;                
            }
        }
    }

    private void Attack()
    {
        Vector3 direction = (player.position - transform.position) + new Vector3(attackOffset.x * Random.Range(-visionStr , visionStr),
            attackOffset.y * Random.Range(-visionStr , visionStr) , attackOffset.z * Random.Range(-visionStr , visionStr));
        attackManager.PerformAttack();
        if ((Physics.Raycast(rayPoint.position, direction.normalized, out RaycastHit hit, inputs.attackRange * 2)) &&
            hit.transform.CompareTag("Player"))
        {
            Debug.Log("player got hit");
            if (playerHit == null)
            {
                player.GetComponent<EntityHitManager>();
            }
            playerHit.TakeDamage(enemy);
        }

    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(new Vector3(transform.position.x , transform.position.y +0.5f , transform.position.z) , agent.destination , Color.blue);
    }

    void RotateTowardsTarget(Transform source)
    {
        Vector3 directionToTarget = player.position - source.position;
        directionToTarget.y = 0;
        if (directionToTarget.sqrMagnitude > 0.001f)
        {
            Vector3 newDirection = Vector3.RotateTowards(source.forward, directionToTarget,Mathf.Infinity,0f);
            source.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
