using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NavmeshMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public Vector3 velocity { get; private set; }
    [SerializeField] private Transform player;
    private EnemyInputs inputs;
    //[field: SerializeField] public float attackSpeed { get; set; }
    [SerializeField]private float attackCd;
    private EntityAttackManager attackManager;
    [SerializeField]private Vector3 attackOffset;
    private EntityHitManager playerHit = null;
    [SerializeField] private LayerMask detectionLayer;
    private Entity enemy;
    public float visionStr = 1f;
    public float rotationSpeed = 180f;
    [SerializeField] private Transform rayPoint;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        inputs = GetComponent<EnemyInputs>();
        enemy = GetComponent<Entity>();
        attackManager = GetComponent<EntityAttackManager>();
        attackCd = inputs.attackSpeed;
    }
    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(agent.velocity);
        inputs.velocity = agent.velocity;
        RotateTowardsTarget(rayPoint);
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
            if ((attackCd -= Time.deltaTime) <= 0)
            {
                attackCd = inputs.attackSpeed; 
               // Debug.Log("herererere");
                Attack();
                               
            }

            //Debug.Log("rotating");
        }
    }

    private void Attack()
    {
        Vector3 direction = (player.position - transform.position) + new Vector3(attackOffset.x * Random.Range(-visionStr , visionStr),
            attackOffset.y * Random.Range(-visionStr , visionStr) , attackOffset.z * Random.Range(-visionStr , visionStr));
        attackManager.PerformAttack();
        if ((Physics.Raycast(rayPoint.position, direction.normalized, out RaycastHit hit, inputs.attackRange * 2 , detectionLayer)) &&
            hit.transform.CompareTag("Player"))
        {
            //Debug.Log("player got hit");
            if (playerHit == null)
            {
                playerHit = hit.collider.gameObject.GetComponent<EntityHitManager>();
            }
            playerHit.TakeDamage(enemy);
        }

    }
    /*
    private void OnDrawGizmos()
    {
        Debug.DrawLine(new Vector3(transform.position.x , transform.position.y +0.5f , transform.position.z) , agent.destination , Color.blue);
    }
    */

    void RotateTowardsTarget(Transform source)
    {
        Vector3 directionToTarget = player.position - source.position;
        directionToTarget.y = 0;
        if (directionToTarget.sqrMagnitude > 0.001f)
        {
            float degree = rotationSpeed * Mathf.Deg2Rad;
            //Debug.Log();
            Vector3 newDirection = Vector3.RotateTowards(source.forward, directionToTarget,120*Time.deltaTime,0f);
            source.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
