using System;
using System.Collections.Generic;
using UnityEngine;

public class FistCollision : MonoBehaviour
{
    [SerializeField]
    private Entity attacker;
    [SerializeField]
    private EntityStats stats;

    public bool canAttack { get; set; }
    public Dictionary<GameObject, bool> hitMap = new Dictionary<GameObject, bool>();
    private EntityAttackManager _attackManager;
    private void Awake()
    {
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

        _attackManager = GetComponentInParent<EntityAttackManager>();

    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("here");
        if (canAttack && other.gameObject != null && other.gameObject.CompareTag("Enemy"))
        {
            hitMap.TryAdd(other.gameObject, true);
            if (hitMap[other.gameObject])
            {
                EntityHitManager hitManager = other.gameObject.GetComponent<EntityHitManager>();
                EnemyInputs inputs = other.gameObject.GetComponent<EnemyInputs>();
                if (hitManager != null )
                {
                    Debug.Log("performed hit");
                    _attackManager.PerformHit(other.gameObject , other.ClosestPoint(transform.position));
                    inputs.hitDirection = (other.gameObject.transform.position- attacker.transform.position).normalized;
                    hitManager.TakeDamage(attacker);
                }

                hitMap[other.gameObject] = false;

            }
        }
    }
}
