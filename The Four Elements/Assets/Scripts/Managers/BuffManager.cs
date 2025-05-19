using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private EntityStats entity;
    private List<BuffInstance> entityBuffs = new List<BuffInstance>();
    private EffectManager effectManager;
    private float currAttackMultip = 1f;
    private float currDefenseMultip = 1f;
    private float currSpeedMultip = 1f;
    private float currHealthMultip = 1f;

    public Action<Buff> OnBuffAdded { get; set; }
    public Action<Buff> OnBuffRemoved { get; set; }

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
                    entity.ChangeHealth(vBuff.buff.DamageOverTimeAmount , Color.red); 
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
                OnBuffAdded?.Invoke(buff);
                ApplyBuffStatChange(instance);
            }
        }
    }


    private void RemoveBuff(BuffInstance buff)
    {
        if (buff != null)
        {
            entityBuffs.Remove(buff);
            OnBuffRemoved?.Invoke(buff.buff);
            ResetBuff(buff);
        }

    }


    void ApplyBuffsToEntity()
    {
        float[] buffMultips = { currAttackMultip, currDefenseMultip, currHealthMultip, currSpeedMultip }; 
        entity.ApplyBuffMultipliers(buffMultips);
    }

    private void ApplyBuffStatChange(BuffInstance instance)
    {
        currAttackMultip *= instance.buff.attackMultiplier;
        currDefenseMultip *= instance.buff.defenseMultiplier;
        currHealthMultip *= instance.buff.healthMultiplier;
        currSpeedMultip *= instance.buff.speedMultiplier;
        ApplyBuffsToEntity();
        entity.ApplyElementStats();



    }
    private void ResetBuff(BuffInstance instance)
    {
        currAttackMultip *= 1/instance.buff.attackMultiplier;
        currDefenseMultip *= 1/instance.buff.defenseMultiplier;
        currHealthMultip *= 1/instance.buff.healthMultiplier;
        currSpeedMultip *= 1/instance.buff.speedMultiplier;
        ApplyBuffsToEntity();
        entity.ApplyElementStats();
    }
}
