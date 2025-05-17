using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEvents : MonoBehaviour
{
    private FistCollision _collision;

    private void Awake()
    {
        _collision = GetComponentInChildren<FistCollision>();

    }

    public void Attack1()
    {
    }

    public void Attack2()
    {
    }

    public void Attack3()
    {
    }

    public void StartCollision()
    {
        //Debug.Log("can hit");
        
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
    }

    public void CloseCollision()
    {
        //Debug.Log("cannot hit");
        _collision.canAttack = false;
    }
    
    

}
