using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityStats : MonoBehaviour
{

    public Action OnStatChange { get;  set; }

    [field:SerializeField]
    public StatDefiner baseStats { get; private set; }
    [field:SerializeField]
    public ElementalType attackElement { get; private set; }
    [field:SerializeField]
    public float attackMultiplier{ get; set; }
    [field:SerializeField]
    public float defenseMultiplier{ get;  set; }
    [field:SerializeField]
    public float healthMultiplier { get; set; }
    [field:SerializeField]
    public float speedMultiplier{ get;  set; }


    [SerializeField] private float buffAttackMultiplier = 1f;
    [SerializeField] private float buffDefenseMultiplier = 1f;
    [SerializeField] private float buffHealthMultiplier = 1f;
    [SerializeField] private float buffSpeedMultiplier = 1f;

    [field:SerializeField] public ElementData element { get; private set; }
    [field:SerializeField]public float currentHealth{ get; set; }

    private void Awake()
    {
        currentHealth = baseStats.MaxHealth;
        ApplyElementStats();
    }

    public float CalculateFinalDamage(EntityStats enemy)
    {
        float elemRes = 0f;
        switch (attackElement)
        {
            case ElementalType.FIRE:
                elemRes = enemy.baseStats.FireResistance;
                break;
            case ElementalType.SOIL:
                elemRes = enemy.baseStats.SoilResistance;
                break;
            case ElementalType.WIND:
                elemRes = enemy.baseStats.WindResistance;
                break;
            case ElementalType.WATER:
                elemRes = enemy.baseStats.WaterResistance;
                break;
            default:
                elemRes = 1f;
                break;
        }
       // Debug.Log(elemRes +" " + enemy.GetFinalDef() );
        return GetFinalAttack() / (elemRes * enemy.GetFinalDef()) * Random.Range(0.8f,1f);
    }
    public float GetFinalDef()
    {
        return baseStats.BaseDefense * defenseMultiplier;
    }

    public float GetFinalAttack()
    {
        return baseStats.BaseAttack * attackMultiplier;
    }

    public void SwitchElement(ElementData switchElement)
    {
        element = switchElement;
    }

    public void ApplyBuffMultipliers(float[] buffMultips)
    {
        buffAttackMultiplier = buffMultips[0];
        buffDefenseMultiplier = buffMultips[1];
        buffHealthMultiplier = buffMultips[2];
        buffSpeedMultiplier = buffMultips[3];
    }

    public void ApplyElementStats()
    {
        attackElement = element.elementType;
        attackMultiplier = element.attackMultiplier * buffAttackMultiplier;
        defenseMultiplier = element.defenseMultiplier * buffDefenseMultiplier;
        healthMultiplier = element.healthMultiplier * buffHealthMultiplier;
        speedMultiplier = element.speedMultiplier * buffSpeedMultiplier;
        OnStatChange?.Invoke();
        // currentHealth *= healthMultiplier;
    }

    public float ChangeHealth(float hit)
    {
        currentHealth -= hit;
        return currentHealth;
    }
}
