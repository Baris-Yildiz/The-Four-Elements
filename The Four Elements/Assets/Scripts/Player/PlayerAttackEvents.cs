using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEvents : MonoBehaviour
{
    private FistCollision _collision;

    private void Awake()
    {
        _collision = GetComponentInChildren<FistCollision>(true);

    }

  

    public void StartCollision()
    {
       // Debug.Log("can hit");
      //  Debug.Log(_collision.hitMap == null);
        var keysCopy = new List<GameObject>(_collision.hitMap.Keys);

        foreach (var key in keysCopy)
        {
            if (key != null)
            {
                _collision.hitMap[key] = true;
            }
            else
            {
                _collision.hitMap.Remove(key);
            }
        }
        _collision.canAttack = true;
    //    Debug.Log(_collision.canAttack);
    }

    public void CloseCollision()
    {
       // Debug.Log("cannot hit");
        _collision.canAttack = false;
       
    }
    
    

}
