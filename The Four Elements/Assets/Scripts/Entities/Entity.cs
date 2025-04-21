using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour 
{
   [field:SerializeField]
    public EntityStats stats { get; private set; }
    
    protected virtual void Awake()
    {
        
        
    }

    

   

    

    public virtual void Die()
    {
    }
    
    


    

}
