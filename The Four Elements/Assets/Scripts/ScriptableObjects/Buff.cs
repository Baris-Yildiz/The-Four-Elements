using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public enum EffectType { Buff, Debuff }

[CreateAssetMenu(fileName = "NewStatDefinition", menuName = "Buff")]
public class Buff : ScriptableObject
{
    public string buffId;
    public EffectType effectType;
    public float duration;
    public Sprite effectIcon;
    /*
    public Sprite effectIcon;
    public AudioClip audio;
    public bool audioLoop;
    public VisualEffectAsset vfx;
    public float vfxDuration;
    */
    //public List<StatModifier> TemporaryStatModifiers; // e.g., Slow reduces speed stat

    public float attackMultiplier;
    public float defenseMultiplier;
    public float healthMultiplier;
    public float speedMultiplier;
    public bool isRefreshable;
    public float DamageOverTimeAmount;
    //public ElementalType DamageOverTimeType = ElementalType.BASE;
    public float DamageOverTimeTickRate = 1f;
}
