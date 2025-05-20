using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour 
{
    public EntityStats stats { get; private set; }
    [field:SerializeField]public Material OnDieMaterial { get; private set; } //should be BurnShader
   
    
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
        var meshRenderers = GetComponentsInChildren<MeshRenderer>();

        Material instancedMaterial = new Material(material); // unique copy
        instancedMaterial.SetFloat("_startTime", Time.unscaledTime);
        //Debug.LogWarning(instancedMaterial.GetFloat("_startTime"));

        foreach (var skin in skins)
        {
            skin.materials = new Material[] { instancedMaterial };
        }

        foreach (var mr in meshRenderers)
        {
            mr.materials = new Material[] { instancedMaterial };
        }

        Destroy(gameObject, 2f);
    }
}
