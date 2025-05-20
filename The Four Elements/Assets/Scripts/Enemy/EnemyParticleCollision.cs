using System;
using UnityEngine;

public class EnemyParticleCollision : MonoBehaviour
{
    private EntityStats player;
    private Entity player1;

    private EntityHitManager _hitManager;

    private float attackMultip = 5f;

    private void Awake()
    {
        _hitManager = GetComponent<EntityHitManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();
        player1 = player.GetComponent<Entity>();
    }

    private void OnParticleCollision(GameObject other)
    {
        player.ChangeAttack(attackMultip);
        _hitManager.TakeDamage(player1);
        player.ChangeAttack(1/attackMultip);
        
    }
}
