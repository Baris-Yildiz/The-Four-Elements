using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour 
{
    public EntityStats stats { get; private set; }
    
    protected virtual void Awake()
    {
        stats = GetComponent<EntityStats>();


    }

    

   

    

    public virtual void Die()
    {
        Destroy(gameObject);
    }
    
    


    

}
