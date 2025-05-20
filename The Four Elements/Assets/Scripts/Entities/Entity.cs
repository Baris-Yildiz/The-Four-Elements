using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour 
{
    public EntityStats stats { get; private set; }
    public Material OnDieMaterial { get; private set; } //should be BurnShader
   
    
    protected virtual void Awake()
    {
        stats = GetComponent<EntityStats>();
    }
    public virtual void Die()
    {
        ChangeMaterialTo(OnDieMaterial);
       
    }

    private void ChangeMaterialTo(Material material)
    {
        var skins = GetComponentsInChildren<SkinnedMeshRenderer>();
        material.SetFloat("_startTime", Time.time);

        foreach (var skin in skins)
        {
            skin.materials = new Material[] { material };
        }

        var meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var mr in meshRenderers)
        {
            mr.materials = new Material[] { material };
        }
        Destroy(gameObject , 2f);
    }
}
