using System;
using System.Collections;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private EnemyInputs enemy;
    [SerializeField] private float detectionFreq = 0.5f;
    [SerializeField] private Transform player;
    [SerializeField] private float viewAngle;
    [SerializeField] private float detectedAngle = 360f;
    [SerializeField] private float viewDistance;
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float stopTime = 3f;
    private float remainingStopTime;
    [SerializeField] private float tempAngle;
    [SerializeField] private Transform rayPoint;
    private bool canDetect = true;
    public bool isPlayerDetected = false;

    private float cosHalfViewAngle;  // Cosine of half of view angle

    private void Awake()
    {
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
            tempAngle = viewAngle;
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
            
            enemy.lastPosition = new Vector3(hit.transform.position.x, 0, hit.transform.position.z);
            enemy.playerDetected = true;
            remainingStopTime = stopTime;
            tempAngle = detectedAngle;
            cosHalfViewAngle = Mathf.Cos(tempAngle * 0.5f * Mathf.Deg2Rad);
        }
        else
        {
            enemy.playerDetected = false;
        }
    }
}
