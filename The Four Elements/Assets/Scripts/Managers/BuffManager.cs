using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private EntityStats entity;
    private List<BuffInstance> entityBuffs = new List<BuffInstance>();
    private EffectManager effectManager;


    private void Start()
    {
        entity = GetComponent<EntityStats>();
    }

    private void Update()
    {
        ProcessBuffs();
    }

    private void ProcessBuffs()
    {
        List<BuffInstance> buffsToRemove = new List<BuffInstance>();

        foreach (var vBuff in entityBuffs)
        {
            if (vBuff.IsBuffOver(Time.deltaTime))
            {
                buffsToRemove.Add(vBuff);
            }
            else
            {
                if (vBuff.isDot && vBuff.IsTickTime(Time.deltaTime))
                {
                   // Debug.Log("Dot is being applied");
                    entity.ChangeHealth(vBuff.buff.DamageOverTimeAmount);
                }
            }
        }

        foreach (var buff in buffsToRemove)
        {
            RemoveBuff(buff);
        }
    }

    public void AddBuff(Buff buff)
    {
        if (buff != null)
        {
            bool buffExists = false;
            foreach (var _buff in entityBuffs )
            {
                if (_buff.buff == buff)
                {
                    _buff.Refresh();
                    buffExists = true;
                    break;
                }
            }
            if (!buffExists)
            {
                BuffInstance instance = new BuffInstance(buff);
                entityBuffs.Add(instance);
                ApplyBuffStatChange(instance);
            }
        }
    }


    private void RemoveBuff(BuffInstance buff)
    {
        if (buff != null)
        {
            entityBuffs.Remove(buff);
            ResetBuff(buff);
        }

    }

    private void ApplyBuffStatChange(BuffInstance buffInstance)
    {
        if (buffInstance != null)
        {
            entity.attackMultiplier *= buffInstance.buff.attackMultiplier;
            entity.defenseMultiplier *= buffInstance.buff.defenseMultiplier;
            entity.healthMultiplier *= buffInstance.buff.healthMultiplier;
            entity.speedMultiplier *= buffInstance.buff.speedMultiplier;

        }
    }
    private void ResetBuff(BuffInstance buffInstance)
    {
        if (buffInstance != null)
        {
            entity.attackMultiplier *= 1/buffInstance.buff.attackMultiplier;
            entity.defenseMultiplier *= 1/buffInstance.buff.defenseMultiplier;
            entity.healthMultiplier *= 1/buffInstance.buff.healthMultiplier;
            entity.speedMultiplier *= 1/buffInstance.buff.speedMultiplier;

        }
    }
}
