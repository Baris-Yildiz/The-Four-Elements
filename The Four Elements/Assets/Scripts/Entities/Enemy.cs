using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{

    public EnemyInputs _inputs { get; private set; }
   
    


    protected override void Awake()
    {
        _inputs = GetComponent<EnemyInputs>();
        base.Awake();
    }
}
