using UnityEngine;
using System;

public class EntityHitManager : MonoBehaviour,IDamageable
{
    [SerializeField]
    private EntityStats entityStats;

    private BuffManager buffManager;
    public event Action OnEntityDied;
    public event Action<float> OnHealthChanged;
    public event Action OnGotHit; 
    [SerializeField]
    private EffectManager effectManager;

    private void Awake()
    {
        entityStats = GetComponent<EntityStats>();
        effectManager = GetComponent<EffectManager>();
        buffManager = GetComponent<BuffManager>();
    }

    public void TakeDamage(Entity attacker)
    {
       // Debug.Log(attacker );
        //Debug.Log(entityStats);
        if (attacker == null)
        {
            Debug.Log("attacker is null");
            return;
        }

        if (entityStats == null)
        {
            Debug.Log("entity stats is null");
            return;
        }

        if (attacker.stats == null)
        {
            Debug.Log("attacker stats is null");
        }

        float damage = attacker.stats.CalculateFinalDamage(entityStats);
      //  Debug.Log("Enemy current health is : " + entityStats.currentHealth);
        
        if (entityStats.ChangeHealth(damage) <= 0)
        {
            OnEntityDied?.Invoke();
            //return;
        }
        
        buffManager.AddBuff(attacker.stats.element.OnHitEffectDefinition);
        OnHealthChanged?.Invoke(attacker.stats.currentHealth);
        OnGotHit?.Invoke();
    }

}

    


