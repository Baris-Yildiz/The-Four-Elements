using System;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private EnemyInputs enemy;
    [SerializeField] private float detectionFreq = 0.5f;
    [SerializeField] private Transform player;
    [SerializeField] private float viewAngle;
    [SerializeField] private float viewDistance;
    [SerializeField] private Transform rayPoint;
    private bool canDetect = true;
    public bool isPlayerDetected = false;

    private void Awake()
    {
        enemy = GetComponent<EnemyInputs>();
    }


    private void Update()
    {
        if (detectionFreq > 0 &&(detectionFreq -= Time.deltaTime) <= 0)
        {
            canDetect = true;
        }
        CheckPlayer();
    }

    private void CheckLos()
    {
        if ((!enemy.playerDetected && Vector3.Distance(transform.position, enemy.lastPosition) <= 1f))
        {
            DetectionCheck();
            
        }
    }
    
    private void CheckPlayer()
    {
        if (canDetect)
        {
            DetectionCheck();
            enemy.chasePlayer =(enemy.playerDetected && Vector3.Distance(transform.position, enemy.lastPosition) > enemy.attackRange) 
                               || (!enemy.playerDetected && Vector3.Distance(transform.position , enemy.lastPosition) <= 1f);
        }
    }

    private void DetectionCheck()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);
        detectionFreq = 0.5f;
        canDetect = false;
        //Debug.Log("detection check");
        /*
        if ((Physics.Raycast(rayPoint.position, direction.normalized, out RaycastHit hit1, viewDistance)))
        {
            //Debug.Log("lalalalalalalala " + angle);
            Debug.Log(hit1.transform.tag + " hitted target tag");
        }
        */
        
        
        Debug.Log(angle + " " + viewAngle/2 );
       // Debug.Log((Physics.Raycast(rayPoint.position, direction.normalized, out RaycastHit hit2, viewDistance)));
        
        if (angle <= viewAngle / 2 &&
            (Physics.Raycast(rayPoint.position, direction.normalized, out RaycastHit hit, viewDistance)) && (hit.transform.CompareTag("Player"))
           ) 
        {
            Debug.Log("detection check success " + hit.transform.position);
            enemy.lastPosition =new Vector3(hit.transform.position.x , 0 , hit.transform.position.z);
            enemy.playerDetected = true;
                
        }
        else
        {
            Debug.Log("detection failed");
            enemy.playerDetected = false;
        }
        
    }

    private void OnDrawGizmos()
    {
        
        Vector3 direction = player.position - transform.position;
        Debug.DrawRay(rayPoint.position, direction.normalized * 10, Color.green);
        //Debug.DrawLine(rayPoint.position , player.position, Color.red);
    }
}
