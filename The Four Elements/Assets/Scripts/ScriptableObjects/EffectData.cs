using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "EffectData", menuName = "EffectData")]
public class EffectData : ScriptableObject
{
    public Sprite effectIcon;
    public AudioClip audio;
    public bool audioLoop;
    public VisualEffectAsset vfx;
    public float vfxDuration;
    
}
