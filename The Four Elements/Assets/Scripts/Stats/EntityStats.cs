using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityStats : MonoBehaviour
{
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

    [field:SerializeField] public ElementData element { get; private set; }
    public float currentHealth{ get; set; }

    private void Awake()
    {
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
    protected void ApplyElementStats()
    {
        attackElement = element.elementType;
        attackMultiplier = element.attackMultiplier;
        defenseMultiplier = element.defenseMultiplier;
        healthMultiplier = element.healthMultiplier;
        speedMultiplier = element.speedMultiplier;
        currentHealth = baseStats.MaxHealth* healthMultiplier;
    }

    public float ChangeHealth(float hit)
    {
        currentHealth -= hit;
        return currentHealth;
    }
}
