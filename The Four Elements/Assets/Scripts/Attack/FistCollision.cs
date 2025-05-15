using System;
using UnityEngine;

public class FistCollision : MonoBehaviour
{
    [SerializeField]
    private Entity attacker;
    [SerializeField]
    private EntityStats stats;

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

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("here");
        if (other.gameObject != null && other.gameObject.CompareTag("Enemy"))
        {
            
            EntityHitManager hitManager = other.gameObject.GetComponent<EntityHitManager>();
            EnemyInputs inputs = other.gameObject.GetComponent<EnemyInputs>();
            if (hitManager != null )
            {
                _attackManager.PerformHit(other.gameObject , other.ClosestPoint(transform.position));
                inputs.hitDirection = (other.gameObject.transform.position- attacker.transform.position).normalized;
                hitManager.TakeDamage(attacker);
            }

        }
    }
}
