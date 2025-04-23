using System;
using System.Collections.Generic;
using UnityEngine;

// Example script (could be part of your StateMachine, Player controller, or a dedicated manager)
public class PlayerDetection : MonoBehaviour
{
    [Header("Detection Settings")]
    [Tooltip("The radius around the player to check for enemies.")]
    [SerializeField] private float detectionRadius = 30.0f;

    [Tooltip("The physics layer(s) that enemies reside on.")]
    [SerializeField] private LayerMask enemyLayerMask; // Assign in Inspector!

    [Tooltip("How often (in seconds) to perform the check.")]
    [SerializeField] private float checkInterval = 0.25f; // Check 4 times per second

    [Header("References (Assign or Get)")]
    [SerializeField] private StateMachine stateMachine; // Reference to your state machine
    [SerializeField] private Player player; // Reference to your player data/states script

    [SerializeField] private float stanceChangeTime = 3f;

    private float remainingStanceChangeTime;
    // --- Runtime ---
    private float _timeSinceLastCheck = 0f;
    private Collider[] _overlapResults = new Collider[10]; // Pre-allocate array to reduce garbage
    

    void Awake()
    {
        remainingStanceChangeTime = stanceChangeTime;
        if (player == null) player = GetComponent<Player>();
    }

    void Update()
    {
        // Only perform check periodically
        _timeSinceLastCheck += Time.deltaTime;
        if (_timeSinceLastCheck >= checkInterval)
        {
            _timeSinceLastCheck = 0f; // Reset timer
            CheckForNearbyEnemies();
        }

        if (remainingStanceChangeTime > 0)
        {
            remainingStanceChangeTime -= Time.deltaTime;
            if (remainingStanceChangeTime <= 0)
            {
                Debug.Log("Exiting Combat Stance");
                player.IsCombatState = false;
                player.target = null;
            }
        }
    }

    private void CheckForNearbyEnemies()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(
            transform.position,
            detectionRadius,   
            _overlapResults, 
            enemyLayerMask     
        );
        
        if(hitCount >0)
        {
            //Debug.Log("Enemies Detected : " + hitCount);
            float minDist = detectionRadius+1;
            //Debug.Log(_overlapResults);

            foreach (var _enemy in _overlapResults)
            {
                if (_enemy != null)
                {
                    float dist =
                        Math.Abs(Vector3.Distance(player.transform.position, _enemy.gameObject.transform.position));
                    if (dist <= minDist)
                    {
                        minDist = dist;
                        player.target = _enemy.gameObject.transform;
                    }
                }
            }

            
            player.IsCombatState = true;
            remainingStanceChangeTime = stanceChangeTime;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}