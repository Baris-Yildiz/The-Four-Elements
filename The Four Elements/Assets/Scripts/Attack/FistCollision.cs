using System;
using System.Collections.Generic;
using UnityEngine;

public class FistCollision : MonoBehaviour
{
    
    private Entity attacker;
    private EntityStats stats;
    public bool canAttack { get; set; }
    public Dictionary<GameObject, bool> hitMap = new Dictionary<GameObject, bool>();
    private EntityAttackManager _attackManager;
    private void Awake()
    {

        attacker = GetComponentInParent<Entity>();
        stats = GetComponentInParent<EntityStats>();
        _attackManager = GetComponentInParent<EntityAttackManager>();
        /*
        attacker = GetComponent<Entity>();
        stats = GetComponent<EntityStats>();
        */
        if (attacker == null )
        {
            Debug.Log("Attacker is null");
        }
        if (stats == null )
        {
            Debug.Log("Entity Stats is null");
        }

       

    }

   

    private void OnTriggerStay(Collider other)
    {
       // Debug.Log(canAttack + " " + other.gameObject.CompareTag("Enemy"));
        if (canAttack && other.gameObject != null && other.gameObject.CompareTag("Enemy"))
        {
            
            hitMap.TryAdd(other.gameObject, true);
            if (hitMap[other.gameObject])
            {
                
                EntityHitManager hitManager = other.gameObject.GetComponent<EntityHitManager>();
                EnemyInputs inputs = other.gameObject.GetComponent<EnemyInputs>();
                if (hitManager != null )
                {
                  //  Debug.Log("performed hit");
                    Vector3 p = other.ClosestPoint(transform.position);
                    _attackManager.PerformHit(other.gameObject , p);
                    hitManager.CalculateHitPoint(p);
                    Vector3 worldHitDirection = (other.gameObject.transform.position - attacker.transform.position).normalized;
                    Vector3 localHitDirection = other.transform.InverseTransformDirection(worldHitDirection);
                    inputs.hitDirection = localHitDirection;
                   // Debug.Log(inputs.hitDirection);
                    hitManager.TakeDamage(attacker);
                }

                hitMap[other.gameObject] = false;

            }
        }
    }
}
