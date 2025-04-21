using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private Entity player;
    [SerializeField]
    private Entity target;

    private EffectManager effectManager;

    private void Awake()
    {
        effectManager = GetComponent<EffectManager>();
    }
    
    private List<SkillInstance> skills = new List<SkillInstance>();
    
    
    
    
    
    
}
