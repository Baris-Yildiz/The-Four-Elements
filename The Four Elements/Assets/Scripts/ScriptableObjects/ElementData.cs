using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewElementData", menuName = "Elements/Element Data")]
public class ElementData : ScriptableObject
{
    public ElementalType elementType = ElementalType.BASE;
    public string ElementName ;
    
    public float attackMultiplier;
    public float defenseMultiplier;
    public float healthMultiplier;
    public float speedMultiplier;
    
    [Header("On-Hit Effect")]
    [Tooltip("The Buff/Debuff definition applied when an attack of this element hits.")]
    public Buff OnHitEffectDefinition;
}