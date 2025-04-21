using UnityEngine;


[CreateAssetMenu(fileName = "NewStatDefinition", menuName = "Stats/Character Stats Definition")]
public class StatDefiner : ScriptableObject
{
    // --- Identity ---
    [Header("Identity")]
    [Tooltip("Display name or type identifier for this definition (e.g., 'Goblin Archer', 'Player').")]
    public string EntityTypeName = "DefaultEntity";

    [Tooltip("The innate/default element of this entity type (can be None).")]
    public ElementalType BaseElementType = ElementalType.BASE;

    // --- Core Base Stats ---
    [Header("Core Base Stats")]
    [Tooltip("Base maximum health value before any modifiers.")]
    [Min(1)] // Ensure health is positive
    public float MaxHealth = 100f;

    [Tooltip("Base attack power value before any modifiers.")]
    [Min(0)]
    public float BaseAttack = 10f;

    [Tooltip("Base defense value before any modifiers.")]
    [Min(0)]
    public float BaseDefense = 5f;

    [Tooltip("Base movement speed value before any modifiers.")]
    [Min(0)]
    public float BaseSpeed = 5f;
 
    
    [Header("Base Resistances")]
    [Tooltip("Damage multiplier for incoming Fire damage (1 = normal, 0.5 = resist, 2 = weak, 0 = immune).")]
    [Range(0f, 2f)] public float FireResistance = 1.0f;

    [Tooltip("Damage multiplier for incoming Water damage.")]
    [Range(0f, 2f)] public float WaterResistance = 1.0f;

    [Tooltip("Damage multiplier for incoming Soil damage.")]
    [Range(0f, 2f)] public float SoilResistance = 1.0f;

    [Tooltip("Damage multiplier for incoming Wind damage.")]
    [Range(0f, 2f)] public float WindResistance = 1.0f;

    public float GetBaseResistance(ElementalType elementType)
    {
        switch (elementType)
        {
            case ElementalType.FIRE: return FireResistance;
            case ElementalType.WATER: return WaterResistance;
            case ElementalType.SOIL: return SoilResistance;
            case ElementalType.WIND: return WindResistance;
            default:
                return 1.0f; 
        }
    }
}
