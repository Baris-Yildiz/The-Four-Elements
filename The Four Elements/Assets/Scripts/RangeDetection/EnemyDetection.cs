using System;
using System.Collections;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private EnemyInputs enemy;
    [SerializeField] private float detectionFreq = 1f;
     private Transform player;
    [SerializeField] private float viewAngle=210f;
    [SerializeField] private float detectedAngle = 360f;
    [SerializeField] private float viewDistance = 15f;
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float stopTime = 3f;
    private float remainingStopTime;
    private float tempAngle;
    private Transform rayPoint;
    private bool canDetect = true;

    private float cosHalfViewAngle;  // Cosine of half of view angle

    private void Awake()
    {
        rayPoint = transform.Find("RayPoint");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tempAngle = viewAngle;
        remainingStopTime = stopTime;
        enemy = GetComponent<EnemyInputs>();
        cosHalfViewAngle = Mathf.Cos(tempAngle * 0.5f * Mathf.Deg2Rad);
    }

    private void Update()
    {
        if (detectionFreq > 0 && (detectionFreq -= Time.deltaTime) <= 0)
        {
            canDetect = true;
        }

        CheckPlayer();

        UpdateViewAngle();
    }

    private void UpdateViewAngle()
    {
        if (!enemy.playerDetected && (remainingStopTime -= Time.deltaTime) <= 0)
        {
           // Debug.LogWarning("aldsadsadasdsadasfsa");
            tempAngle = viewAngle;
            enemy.canAttack = false;
            enemy.startRotation = false;
            remainingStopTime = stopTime;
            cosHalfViewAngle = Mathf.Cos(tempAngle * 0.5f * Mathf.Deg2Rad);
        }
    }

    private void CheckPlayer()
    {
        if (canDetect)
        {
            
            DetectionCheck();
            float sqrToLastPos = (transform.position - enemy.lastPosition).sqrMagnitude;
            enemy.chasePlayer = (enemy.playerDetected && sqrToLastPos > enemy.attackRange * enemy.attackRange) ||
                                (!enemy.playerDetected && sqrToLastPos <= 1f * 1f);
          
        }
    }

    private void DetectionCheck()
    {
        Vector3 direction = player.position - transform.position;
        float sqrDist = direction.sqrMagnitude;
      
        Vector3 dirNormalized = direction / Mathf.Sqrt(sqrDist); 
        float dot = Vector3.Dot(transform.forward, dirNormalized);
        detectionFreq = 1f;
        canDetect = false;
        if (dot >= cosHalfViewAngle &&
            Physics.Raycast(rayPoint.position, dirNormalized, out RaycastHit hit, viewDistance , detectionLayer) &&
            hit.transform.CompareTag("Player"))
        {
            
            enemy.lastPosition = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z);
            enemy.playerDetected = true;
            remainingStopTime = stopTime;
            tempAngle = detectedAngle;
            cosHalfViewAngle = Mathf.Cos(tempAngle * 0.5f * Mathf.Deg2Rad);
        }
        else
        {
           // Debug.LogWarning("not detecdes");
            enemy.playerDetected = false;
        }
    }
}
