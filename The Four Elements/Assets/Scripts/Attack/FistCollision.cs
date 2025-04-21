using System;
using UnityEngine;

public class FistCollision : MonoBehaviour
{
    [SerializeField]
    private Entity attacker;
    [SerializeField]
    private EntityStats stats;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("here");
        if (other.gameObject != null && other.gameObject.CompareTag("Enemy"))
        {
            EntityHitManager hitManager = other.gameObject.GetComponent<EntityHitManager>();
            if (hitManager != null)
            {
                hitManager.TakeDamage(attacker);
                
            }

        }
    }
}
