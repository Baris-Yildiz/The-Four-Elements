using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NavmeshMovement : MonoBehaviour
{
   
    private NavMeshAgent agent;
    private Transform player;
    private EnemyInputs inputs;
    //[field: SerializeField] public float attackSpeed { get; set; }
    [SerializeField]private float attackCd;
    private EntityAttackManager attackManager;
    [SerializeField]private Vector3 attackOffset;
    private EntityHitManager playerHit = null;
    [SerializeField] private LayerMask detectionLayer;
    private Entity enemy;
    public float visionStr = 1f;
   // public float rotationSpeed = 180f;
    private Transform rayPoint;
   
    private void Awake()
    {
        rayPoint = transform.Find("RayPoint");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        inputs = GetComponent<EnemyInputs>();
        enemy = GetComponent<Entity>();
        attackManager = GetComponent<EntityAttackManager>();
        
        
    }
    
    // Update is called once per frame
    void Update()
    {
       // Debug.LogWarning("speed: " + inputs.navSpeed);
        //Debug.LogWarning("accleration: " + inputs.navAcceleration);
        agent.speed = inputs.navSpeed;
        agent.acceleration = inputs.navAcceleration;
        if (inputs.gotHit || inputs.isDead)
        {
            agent.enabled = false;
            return;
        }

        agent.enabled = true;
        inputs.velocity = agent.velocity;
        //RotateTowardsTarget(rayPoint , Mathf.Infinity,false);
       // Debug.Log(agent.pathStatus);
        if ((agent.destination - inputs.hitPosition).sqrMagnitude > 0.1f && (inputs.playerDetected && inputs.chasePlayer || (!inputs.startRotation && !inputs.chasePlayer)))
        {
           // Debug.LogWarning((agent.destination - inputs.hitPosition).sqrMagnitude);
            //Debug.LogWarning(agent.pathStatus);
            agent.SetDestination(inputs.hitPosition);
        }
        else if ((agent.destination - inputs.lastPosition).sqrMagnitude > 0.1f && (!inputs.playerDetected && inputs.chasePlayer))
        {
            Debug.LogWarning("shouldnt be here");
            agent.SetDestination(inputs.lastPosition);
        }
        else if (inputs.playerDetected && inputs.startRotation )
        {
            RotateTowardsTarget(transform ,inputs.rotationSpeed,true);
            if (inputs.canAttack && (attackCd -= Time.deltaTime) <= 0)
            {
                attackCd = inputs.attackSpeed; 
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
            EnemyVFXPooler.Instance.PlayParticle(hit.point , inputs.attackSpeed , inputs.attackColor);
            playerHit.TakeDamage(enemy);
        }

    }
    /*
    private void OnDrawGizmos()
    {
        Debug.DrawLine(new Vector3(transform.position.x , transform.position.y +0.5f , transform.position.z) , agent.destination , Color.blue);
    }
    */

    void RotateTowardsTarget(Transform source , float speed ,bool dotCheck)
    {
       // Debug.LogWarning("rotating");
        Vector3 directionToTarget = player.position - source.position;
        directionToTarget.y = 0;
        if (directionToTarget.sqrMagnitude > 0.001f)
        {
            float degree = speed * Mathf.Deg2Rad;
           // Debug.Log(degree + " degree");
            Vector3 newDirection = Vector3.RotateTowards(source.forward, directionToTarget,degree*Time.deltaTime,0f);
            source.rotation = Quaternion.LookRotation(newDirection);
        }
        if (dotCheck)
        {
            inputs.angle = Vector3.SignedAngle(source.transform.forward, directionToTarget, Vector3.up);
            
        }
    }
}
