using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{
    [Header("Basic Info")]
    public string skillName = "New Skill";
    [TextArea(3, 5)] public string description = "Skill description.";
    public Sprite icon;
    public SkillType skillType = SkillType.NONE;

    [Header("Cost & Cooldown")]
    public float manaCost = 10f;
    public float staminaCost = 0f;
    public float cooldownTime = 5f;

    [Header("Targeting")]
    //public TargetType allowedTargets = TargetType.Enemy;
    public float range = 10f; // Max distance for targeted skills or origin point for AoE/Projectiles
    public bool requiresLineOfSight = true;

    [Header("Damage & Healing")]
    public float baseDamage = 0f; 
    //public DamageType damageType = DamageType.Physical;

    [Header("Area of Effect (AoE)")]
    public float aoeRadius = 0f; // Set > 0 for AoE skills
    // public AoeShape aoeShape = AoeShape.Circle; // Optional shape enum

    [Header("Projectile (If Applicable)")]
    public GameObject projectilePrefab; 
    public float projectileSpeed = 20f;
    public bool projectilePierces = false;

    [Header("Buffs & Debuffs")]
    public Buff buffToApply; 
    //public TargetType buffTarget = TargetType.Enemy; // Who gets the buff (Self, Target)
    public float buffDurationOverride = -1f; 

    [Header("Effects (Assign EffectDefinitionSO Assets)")]
    public EffectData castEffect; 
    public EffectData travelEffect; 
    public EffectData hitEffect; 
    public EffectData missEffect; 

    [Header("Animation")]
    public string animationTriggerName; 
    // public float castTime = 0f; 

    [Header("Requirements")]
    public int requiredLevel = 1;
    // public List<SkillSO> prerequisiteSkills; 

    // --- Potential Helper Methods (Keep logic minimal in SO) ---

    // Example helper to check if it's a damage skill
    public bool IsDamageSkill()
    {
        return baseDamage > 0;
    }

    // Example helper to check if it's an AoE skill
    public bool IsAoESkill()
    {
        return aoeRadius > 0f;
    }

     // Example helper to check if it's a projectile skill
    public bool IsProjectileSkill()
    {
        return projectilePrefab != null;
    }
    
}
